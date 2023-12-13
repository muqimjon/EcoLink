using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Globalization;
using Telegram.Bot.Polling;
using OrgBloom.Bot.Resources;
using Telegram.Bot.Types.Enums;
using OrgBloom.Application.Users.DTOs;
using Microsoft.Extensions.Localization;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.CreateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler(
    ILogger<BotUpdateHandler> logger,
    IServiceScopeFactory serviceScopeFactory,
    IMediator mediator) : IUpdateHandler
{
    private IStringLocalizer localizer  = default!;
    private UserTelegramResultDto user = default!;

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        localizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<BotLocalizer>>();

        user = await GetUserAsync(update);

        var culture = user.LanguageCode switch
        {
            "uz" => new CultureInfo("uz-Uz"),
            "en" => new CultureInfo("en-US"),
            "ru" => new CultureInfo("ru-RU"),
            _ => CultureInfo.CurrentCulture
        };

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        var handler = update.Type switch
        {
            UpdateType.Message => HandleMessageAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => HandleEditedMessageAsync(botClient, update.EditedMessage, cancellationToken),
            UpdateType.CallbackQuery => HandleCallbackQuery(botClient, update.CallbackQuery, cancellationToken),
            UpdateType.InlineQuery => HandleInlineQuery(botClient, update.InlineQuery, cancellationToken),
            _ => HandleUnknownUpdateAsync(botClient, update, cancellationToken)
        };

        try
        {
            await handler;
        }
        catch(Exception ex)
        {
            logger.LogError("HandlePollingError: {ErrorText}", ex.Message);
            await HandlePollingErrorAsync(botClient, ex, cancellationToken);
        }
    }

    private async Task<UserTelegramResultDto> GetUserAsync(Update update)
    {
        var updateContent = BotUpdateHandler.GetUpdateType(update);
        var from = updateContent.From;

        return await mediator.Send(new GetUserByTelegramIdQuery(from.Id))
            ?? await mediator.Send(new CreateUserWithReturnTgResultCommand()
                {
                    IsBot = from.IsBot,
                    TelegramId = from.Id,
                    LastName = from.LastName,
                    Username = from.Username,
                    FirstName = from.FirstName,
                    ChatId = update.Message!.Chat.Id,
                    LanguageCode = from.LanguageCode
                });
    }

    private static dynamic GetUpdateType(Update update)
        => (update.Type switch
        {
            UpdateType.Message => update.Message,
            UpdateType.ChatMember => update.ChatMember,
            UpdateType.ChannelPost => update.ChannelPost,
            UpdateType.InlineQuery => update.InlineQuery,
            UpdateType.MyChatMember => update.MyChatMember,
            UpdateType.CallbackQuery => update.CallbackQuery,
            UpdateType.EditedMessage => update.EditedMessage,
            UpdateType.ShippingQuery => update.ShippingQuery,
            UpdateType.PreCheckoutQuery => update.PreCheckoutQuery,
            UpdateType.EditedChannelPost => update.EditedChannelPost,
            UpdateType.ChosenInlineResult => update.ChosenInlineResult,
            _ => update.Message,
        })!;

    private Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("HandlePollingError: {ErrorText}", exception.Message);

        return Task.CompletedTask;
    }
}
