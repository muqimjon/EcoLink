﻿namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleProfessionAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnRepresentation"] => SendMenuRepresentationAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInvestment"] => SendMenuInvestmentAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnEntrepreneurship"] => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnProjectManagement"] => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
            _ when message.Text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task HandleResendApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnResend"] => ResendApplicationAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task ResendApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        dynamic application = user.Profession switch
        {
            UserProfession.Investor => await investmentAppService.UpdateStatusAsync(user.Id, cancellationToken),
            UserProfession.Entrepreneur => await entrepreneurshipAppService.UpdateStatusAsync(user.Id, cancellationToken),
            UserProfession.ProjectManager => await projectManagementAppService.UpdateStatusAsync(user.Id, cancellationToken),
            UserProfession.Representative => await representationAppService.UpdateStatusAsync(user.Id, cancellationToken),
            _ => default!
        };

        await ClarifyProfessionQueryAsync(botClient, message, cancellationToken, application);
    }

    private async Task ClarifyProfessionQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, dynamic application)
    {
        var handler = user.Profession switch
        {
            UserProfession.Investor => InvestmentApplicationAsync(botClient, message, cancellationToken, application),
            UserProfession.Entrepreneur => EntrepreneurshipApplicationAsync(botClient, message, cancellationToken, application),
            UserProfession.ProjectManager => ProjectManagementQueryAsync(botClient, message, cancellationToken, application),
            UserProfession.Representative => RepresentationApplicationAsync(botClient, message, cancellationToken, application),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };
        try { await handler; }
        catch { logger.LogError("Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task HandleExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);
        Task handler;

        if (message.Text == localizer["rbtnCancel"])
        {
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendRequestForPurposeAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendRequestForPurposeAsync(botClient, message, cancellationToken),
                _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
            };
            user.Expectation = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch { logger.LogError("Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task HandlePurposeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);
        Task handler;

        if (message.Text == localizer["rbtnCancel"])
        {
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendRequestForPhoneNumberAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendRequestForPhoneNumberAsync(botClient, message, cancellationToken),
                _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
            };
            user.Purpose = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch { logger.LogError("Error handling message from {from.FirstName}", user.FirstName); }
    }
}