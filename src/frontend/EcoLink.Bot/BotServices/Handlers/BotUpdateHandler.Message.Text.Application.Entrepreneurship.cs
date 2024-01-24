using EcoLink.ApiService.Models;
using EcoLink.ApiService.Models.Entrepreneurship;

namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSelectedEntrepreneurshipMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnApply"] => EntrepreneurshipApplicationAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInfo"] => SendProfessionInfoAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        user.Profession = UserProfession.Entrepreneur;
    }

    private async Task EntrepreneurshipApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        user.Entrepreneurship ??= await entrepreneurshipService.AddAsync(new EntrepreneurshipDto() { UserId = user.Id }, cancellationToken);

        if (user.Entrepreneurship.IsSubmitted)
            await SendAlreadyExistApplicationAsync(botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAboutProjectForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendRequestForHelpTypeEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
            user.Entrepreneurship.Project = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleAboutHelpTypeForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendRequestForRequiredFundingForEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
            user.Entrepreneurship.HelpType = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleRequiredFundingForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendRequestForAssetsInvestedForEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
            user.Entrepreneurship.RequiredFunding = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleAssetsInvestedForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.Entrepreneur => SendRequestForPhoneNumberAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
            user.Entrepreneurship.AssetsInvested = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}