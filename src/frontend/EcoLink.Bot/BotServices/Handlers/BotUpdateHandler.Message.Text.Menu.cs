﻿namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleMainMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnDepartaments"] => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnSettings"] => SendMenuSettingsAsync(botClient, message, cancellationToken),
            _ when message.Text == localizer["rbtnInfo"] => SendInfoAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnFeedback"] => SendFeedbackMenuQueryAsync(botClient, message, cancellationToken),
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
            { } text when text == localizer["rbtnEditPersonalInfo"] => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
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
            user.LanguageCode = message.Text;

        await SendMenuSettingsAsync(botClient, message, cancellationToken);
    }

    private async Task HandleSelectedFeedbackAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnFeedbackForOrganization"] => SendRequestFeedbackForOrganizationAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnFeedbackForTelegramBot"] => SendRequestFeedbackForTelegramBotAsync(botClient, message, cancellationToken),
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
            { } text when text == localizer["rbtnBack"] => SendMenuSettingsAsync(botClient, message, cancellationToken),
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
                replyToMessageId: message.MessageId,
                text: localizer["txtResponseToFeedback"],
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            await botClient.SendTextMessageAsync(
                chatId: 324168525,
                text: $"Feedback from:\n" +
                $"Name: {user.FirstName}\nLast name: {user.LastName}\nUser name: @{user.Username}\n" +
                $"With Text:{message.Text}",
                cancellationToken: cancellationToken);
        }

        await SendFeedbackMenuQueryAsync(botClient, message, cancellationToken);
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

        await SendFeedbackMenuQueryAsync(botClient, message, cancellationToken);
    }
}
