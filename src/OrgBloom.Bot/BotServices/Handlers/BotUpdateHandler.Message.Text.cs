using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var userState = State.WaitingForSubmitApplication;

        if (message.Text is not "/start")
            userState = await mediator.Send(new GetStateQuery(user.Id), cancellationToken);

        var handler = userState switch
        {
            State.WaitingForSubmitApplication => SendGreetingAsync(botClient, message, cancellationToken),
            State.WaitingForResendApplication => HandleResendApplicationAsync(botClient, message, cancellationToken),
            State.WaitingForSelectMainMenu => HandleMainMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectProfession => HandleProfessionAsync(botClient, message, cancellationToken),
            State.WaitingForSelectSettings => HandleSelectedSettingsAsync(botClient, message, cancellationToken),
            State.WaitingForEnterFirstName => HandleFirstNameAsync(botClient, message, cancellationToken),
            State.WaitingForEnterLastName => HandleLastNameAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPatronomyc => HandlePatronomycAsync(botClient, message, cancellationToken),
            State.WaitingForEnterDateOfBirth => HandleDateOfBirthAsync(botClient, message, cancellationToken),
            State.WaitingForEnterDegree => HandleDegreeAsync(botClient, message, cancellationToken),
            State.WaitingForEnterInvestmentAmount => HandleInvestmentAmountAsync(botClient, message, cancellationToken),
            State.WaitingForEnterEmail => HandleEmailAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPhoneNumber => HandlePhoneNumbeFromTextAsync(botClient, message, cancellationToken),
            State.WaitingForEnterLanguages => HandleLanguagesAsync(botClient, message, cancellationToken),
            State.WaitingForEnterExperience => HandleExperienceAsync(botClient, message, cancellationToken),
            State.WaitingForEnterAddress => HandleAddressAsync(botClient, message, cancellationToken),
            State.WaitingForEnterArea => HandleAreaAsync(botClient, message, cancellationToken),
            State.WaitingForEnterExpectation => HandleExpectationAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPurpose => HandlePurposeAsync(botClient, message, cancellationToken),
            State.WaitingForEnterAboutProject => HandleAboutProjectForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForEnterHelpType => HandleAboutHelpTypeForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForEnterRequiredFunding => HandleRequiredFundingForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForAssetInvested => HandleAssetsInvestedForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForEnterProjectDirection => HandleProjectDirectionForProjectManagementAsync(botClient, message, cancellationToken),
            State.WaitingForSelectLanguage => HandleSentLanguageAsync(botClient, message, cancellationToken),
            State.WaitingForSelectForFeedback => HandleSelectedFeedbackAsync(botClient, message, cancellationToken),
            State.WaitingForSelectPersonalInfo => HandleSelectedPersonalInfoAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleFirstNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if(message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateFirstNameCommand() { Id = user.Id, FirstName = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForLastNameAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleLastNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateLastNameCommand() { Id = user.Id, LastName = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForPatronomycAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandlePatronomycAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text == localizer["rbtnCancel"])
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdatePatronomycCommand() { Id = user.Id, Patronomyc = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForDateOfBirthAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleDateOfBirthAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            if (DateTime.TryParse(message.Text, out DateTime dateOfBirth))
            {
                await mediator.Send(new UpdateDateOfBirthCommand() { Id = user.Id, DateOfBirth = dateOfBirth.ToUniversalTime() }, cancellationToken);
                handler = profession switch
                {
                    UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                    _ => SendRequestForDegreeAsync(botClient, message, cancellationToken)
                };
            }
            else handler = SendRequestForDateOfBirthAsync(botClient, message, cancellationToken);
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleDegreeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateDegreeCommand() { Id = user.Id, Degree = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendRequestForSectorAsync(botClient, message, cancellationToken),
                UserProfession.Entrepreneur => SendRequestForExperienceAsync(botClient, message, cancellationToken),
                _ => SendRequestForLanguagesAsync(botClient, message, cancellationToken),
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandlePhoneNumbeFromTextAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdatePhoneCommand() { Id = user.Id, Phone = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForEmailAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleEmailAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateEmailCommand() { Id = user.Id, Email = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendForSubmitApplicationAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleLanguagesAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateLanguagesCommand() { Id = user.Id, Languages = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForExperienceAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleExperienceAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateExperienceCommand() { Id = user.Id, Experience = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                UserProfession.Entrepreneur => SendRequestForAboutProjectForEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => SendRequestForAddressAsync(botClient, message, cancellationToken), // Works for PM and Representative
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleAddressAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateAddressCommand() { Id = user.Id, Address = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendRequestForAreaAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendRequestForSectorAsync(botClient, message, cancellationToken),
                _ => HandleUnknownMessageAsync(botClient, message, cancellationToken),
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}
