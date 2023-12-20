using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var userState = await mediator.Send(new GetStateQuery(user.Id), cancellationToken);
        var handler = userState switch
        {
            State.None => SendGreetingAsync(botClient, message, cancellationToken),
            State.WaitingForSubmitApplication => SendGreetingAsync(botClient, message, cancellationToken),
            State.WaitingForResendApplication => HandleResendApplicationAsync(botClient, message, cancellationToken),
            State.WaitingForSelectMainMenu => HandleMainMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectProfession => HandleProfessionAsync(botClient, message, cancellationToken),
            State.WaitingForSelectSettings => HandleSelectedSettingsAsync(botClient, message, cancellationToken),
            State.WaitingForSelectLanguage => HandleSentLanguageAsync(botClient, message, cancellationToken),
            State.WaitingForSelectForFeedback => HandleSelectedFeedbackAsync(botClient, message, cancellationToken),
            State.WaitingForSelectPersonalInfo => HandleSelectedPersonalInfoAsync(botClient, message, cancellationToken),
            State.WaitingForFeedbackForOrganization => HandleFeedbackForOrganizationAsync(botClient, message, cancellationToken),
            State.WaitingForFeedbackForTelegramBot => HandleFeedbackForTelegramBotAsync(botClient, message, cancellationToken),
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
            State.WaitingForEnterSector => HandleSectorFromTextAsync(botClient, message, cancellationToken),
            State.WaitingForSelectEntrepreneurshipMenu => HandleSelectedEntrepreneurshipMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectProjectManagementMenu => HandleSelectedProjectManagementMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectInvestmentMenu => HandleSelectedInvestmentMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectRepresentationMenu => HandleSelectedRepresentationMenuAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuSettingsAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            await mediator.Send(new UpdateFirstNameCommand() { Id = user.Id, FirstName = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuSettingsAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            await mediator.Send(new UpdateLastNameCommand() { Id = user.Id, LastName = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendRequestForDateOfBirthAsync(botClient, message, cancellationToken)
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
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdatePatronomycCommand() { Id = user.Id, Patronomyc = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuSettingsAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            if (DateTime.TryParse(message.Text, out DateTime dateOfBirth))
            {
                await mediator.Send(new UpdateDateOfBirthCommand() { Id = user.Id, DateOfBirth = dateOfBirth.ToUniversalTime() }, cancellationToken);
                handler = profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateDegreeCommand() { Id = user.Id, Degree = message.Text.TrimStart('✅').Trim() }, cancellationToken); // TODO: need validation

            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            await mediator.Send(new UpdatePhoneCommand() { Id = user.Id, Phone = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            await mediator.Send(new UpdateEmailCommand() { Id = user.Id, Email = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuSettingsAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            await mediator.Send(new UpdateLanguagesCommand() { Id = user.Id, Languages = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuSettingsAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            await mediator.Send(new UpdateExperienceCommand() { Id = user.Id, Experience = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Entrepreneur => SendRequestForSectorAsync(botClient, message, cancellationToken),
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
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuSettingsAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            await mediator.Send(new UpdateAddressCommand() { Id = user.Id, Address = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendRequestForAreaAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendRequestForSectorAsync(botClient, message, cancellationToken),
                _ => HandleUnknownMessageAsync(botClient, message, cancellationToken),
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleSectorFromTextAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            var handler = profession switch
            {
                UserProfession.Entrepreneur => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuSettingsAsync(botClient, message, cancellationToken),
            };

            try { await handler; }
            catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
        }
        else
        {
            switch (profession)
            {
                case UserProfession.Investor:
                    await mediator.Send(new UpdateInvestorSectorByUserIdCommand { UserId = user.Id, Sector = message.Text }, cancellationToken);
                    await SendRequestForInvestmentAmountForInvestmentAsync(botClient, message, cancellationToken);
                    break;
                case UserProfession.ProjectManager:
                    await mediator.Send(new UpdateProjectManagerProjectDirectionByUserIdCommand() { UserId = user.Id, ProjectDirection = message.Text }, cancellationToken);
                    await SendRequestForExpectationAsync(botClient, message, cancellationToken);
                    break;
                case UserProfession.Entrepreneur:
                    await mediator.Send(new UpdateEntrepreneurSectorByUserIdCommand() { UserId = user.Id, Sector = message.Text }, cancellationToken);
                    await SendRequestForAboutProjectForEntrepreneurshipAsync(botClient, message, cancellationToken);
                    break;
                default:
                    await HandleUnknownMessageAsync(botClient, message, cancellationToken);
                    break;
            };
        }
    }
}