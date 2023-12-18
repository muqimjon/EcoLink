using OrgBloom.Application.Users.Commands.UpdateUsers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
            { } text when text == localizer["rbtnContact"] => SendContactAsync(botClient, message, cancellationToken),
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
            { } text when text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleSentLanguageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        if (!message.Text.Equals(localizer["rbtnBack"]))
            await mediator.Send(new UpdateLanguageCodeCommand()
            {
                Id = user.Id,
                LanguageCode = message.Text switch
                {
                    { } text when text == localizer["rbtnEnglish"] => "en",
                    { } text when text == localizer["rbtnRussian"] => "ru",
                    _ => "uz",
                }

            }, cancellationToken);

        await SendSettingsQueryAsync(botClient, message, cancellationToken);
    }

    private async Task HandleSelectedFeedbackAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnFeedbackForOrganization"] => SendRequestFeedbackForOrganizationQueryAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnFeedbackForTelegramBot"] => SendRequestFeedbackForTelegramBotQueryAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleSelectedPersonalInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnPhoneNumber"] => SendRequestForPhoneNumberAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnEmail"] => SendRequestForEmailAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleFeedbackForTelegramBotAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        if (!message.Text.Equals(localizer["rbtnCancel"]))
        {
            // TO DO Send message to developer with email or telegram bot
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: localizer["txtResponseToFeedback"],
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }

        await SendMainMenuAsync(botClient, message, cancellationToken);
    }

    private async Task HandleFeedbackForOrganizationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        if (!message.Text.Equals(localizer["rbtnCancel"]))
        {
            // TO DO Send message to organization telegram channel or group
            await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtResponseToFeedback"],
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
        }

        await SendMainMenuAsync(botClient, message, cancellationToken);
    }
}
