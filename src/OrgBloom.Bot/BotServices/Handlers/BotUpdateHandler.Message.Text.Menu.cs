using Telegram.Bot;
using Telegram.Bot.Types;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleMainMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnApply"] => SendApplyQueryAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnSettings"] => SendSettingsQueryAsync(botClient, message, cancellationToken),
            _ when message.Text == localizer["rbtnInfo"] => SendInfoAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnFeedback"] => SendFeedbackQueryAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleSelectedSettingsAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnEditLanguage"] => SendSelectLanguageQueryAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnEditPersonalInfo"] => SendEditPersonalInfoQueryAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}
