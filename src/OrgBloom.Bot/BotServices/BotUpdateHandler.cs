using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Localization;
using OrgBloom.Bot.Resources;
using System.Globalization;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler(
    ILogger<BotUpdateHandler> logger,
    IServiceScopeFactory serviceScopeFactory) : IUpdateHandler
{
    private IStringLocalizer localizer;
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogInformation("HandlePollingError: {ErrorText}", exception.Message);

        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var culture = GetCulture(update);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        using var scope = serviceScopeFactory.CreateScope();
        localizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<BotLocalizer>>();

        var handler = update.Type switch
        {
            UpdateType.Message => HandleMessageAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => HandleEditedMessageAsync(botClient, update.EditedMessage, cancellationToken),
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

    private static CultureInfo GetCulture(Update update)
    {
        User? from = update.Type switch
        {
            UpdateType.Message => update.Message?.From,
            UpdateType.ChatMember => update.ChatMember?.From,
            UpdateType.PollAnswer => update.PollAnswer?.User,
            UpdateType.ChannelPost => update.ChannelPost?.From,
            UpdateType.InlineQuery => update.InlineQuery?.From,
            UpdateType.MyChatMember => update.MyChatMember?.From,
            UpdateType.CallbackQuery => update.CallbackQuery?.From,
            UpdateType.EditedMessage => update.EditedMessage?.From,
            UpdateType.ShippingQuery => update.ShippingQuery?.From,
            UpdateType.PreCheckoutQuery => update.PreCheckoutQuery?.From,
            UpdateType.EditedChannelPost => update.EditedChannelPost?.From,
            UpdateType.ChosenInlineResult => update.ChosenInlineResult?.From,
            _ => update.Message?.From,
        };

        return new CultureInfo(from?.LanguageCode ?? "uz-Uz");
    }

    private Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}
