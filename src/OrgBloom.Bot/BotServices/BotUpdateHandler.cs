using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Globalization;
using Telegram.Bot.Polling;
using OrgBloom.Bot.Resources;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Localization;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.CreateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler(
    ILogger<BotUpdateHandler> logger,
    IServiceScopeFactory serviceScopeFactory) : IUpdateHandler
{
    private IStringLocalizer localizer;
    private IMediator mediator;
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogInformation("HandlePollingError: {ErrorText}", exception.Message);

        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        localizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<BotLocalizer>>();

        var culture = await GetCultureAsync(update);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        var handler = update.Type switch
        {
            UpdateType.Message => HandleMessageAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => HandleEditedMessageAsync(botClient, update.EditedMessage, cancellationToken),
            UpdateType.CallbackQuery => HandleCallbackQuery(botClient, update.EditedMessage, cancellationToken),
            UpdateType.InlineQuery => HandleInlineQuery(botClient, update.EditedMessage, cancellationToken),
            _ => HandleUnknownUpdateAsync(botClient, update, cancellationToken)
        };

        try
        {
            await handler;
        }
        catch(Exception ex)
        {
            await HandlePollingErrorAsync(botClient, ex, cancellationToken);
        }
    }

    private async Task<CultureInfo> GetCultureAsync(Update update)
    {
        var updateContent = BotUpdateHandler.GetUpdateType(update);
        var from = updateContent.From;

        if (!await mediator.Send(new IsUserExistByTelegramIdQuery(from.Id)))
            await mediator.Send(new CreateUserCommand()
            {
                TelegramId = from.Id,
                FirstName = from.FirstName,
                LastName = from.LastName,
                Username = from.Username,
                LanguageCode = from.LanguageCode!,
                IsBot = from.IsBot,
                ChatId = update.Message!.Chat.Id
            });

        var languageCode = await mediator.Send(new GetLanguageCodeByTelegramIdQuery(from.Id));
        return new CultureInfo(languageCode ?? "uz-Uz");
    }

    private static dynamic GetUpdateType(Update update)
        => (update.Type switch
        {
            UpdateType.Message => update.Message,
            UpdateType.ChatMember => update.ChatMember,
            UpdateType.PollAnswer => update.PollAnswer,
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
}
