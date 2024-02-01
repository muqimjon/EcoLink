using System.Globalization;
using EcoLink.Bot.Resources;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Localization;
using EcoLink.ApiService.Interfaces.Investment;
using EcoLink.ApiService.Interfaces.Representation;
using EcoLink.ApiService.Interfaces.Entrepreneurship;
using EcoLink.ApiService.Interfaces.ProjectManagement;
using EcoLink.ApiService.Models.Users;
using EcoLink.ApiService.Interfaces.Users;

namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler(
    ILogger<BotUpdateHandler> logger,
    IServiceScopeFactory serviceScopeFactory,
    IUserService service,
    IInvestmentAppService investmentAppService,
    IRepresentationAppService representationAppService,
    IEntrepreneurshipAppService entrepreneurshipAppService,
    IProjectManagementAppService projectManagementAppService) : IUpdateHandler
{
    private IStringLocalizer localizer  = default!;
    private UserDto user = default!;

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        service = scope.ServiceProvider.GetRequiredService<IUserService>();
        localizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<BotLocalizer>>();

        user = await GetUserAsync(update, cancellationToken);
        var culture = user.LanguageCode switch
        {
            "uz" => new CultureInfo("uz-Uz"),
            "en" => new CultureInfo("en-US"),
            "ru" => new CultureInfo("ru-RU"),
            _ => CultureInfo.CurrentCulture
        };

        CultureInfo.CurrentCulture = new CultureInfo("uz-Uz");
        CultureInfo.CurrentUICulture = new CultureInfo("uz-Uz");

        var handler = update.Type switch
        {
            UpdateType.Message => HandleMessageAsync(botClient, update.Message, cancellationToken),
            UpdateType.CallbackQuery => HandleCallbackQuery(botClient, update.CallbackQuery, cancellationToken),
            _ => HandleUnknownUpdateAsync(botClient, update, cancellationToken)
        };

        try { await handler; }
        catch(Exception ex) { await HandlePollingErrorAsync(botClient, ex, cancellationToken); }

        await service.UpdateAsync(user, cancellationToken);
    }

    private async Task<UserDto> GetUserAsync(Update update, CancellationToken cancellationToken)
    {
        var updateContent = BotUpdateHandler.GetUpdateType(update);
        var from = updateContent.From;

        return await service.GetAsync((long)from.Id, cancellationToken)
            ?? await service.AddAsync(new UserDto
            {
                    IsBot = from.IsBot,
                    TelegramId = from.Id,
                    LastName = from.LastName,
                    Username = from.Username,
                    FirstName = from.FirstName,
                    ChatId = update.Message!.Chat.Id,
                    LanguageCode = from.LanguageCode
            }, cancellationToken);
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

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("HandlePollingError: {ErrorText}", exception.Message);
        return Task.CompletedTask;
    }

    private Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}
