namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message.Text);

        var userState = message.Text.Equals("/start") ? State.None : user.State;
        var handler = userState switch
        {
            State.None => SendGreetingAsync(botClient, message, cancellationToken),
            State.WaitingForSelectMainMenu => HandleMainMenuAsync(botClient, message, cancellationToken),  
            State.WaitingForSelectForFeedback => HandleSelectedFeedbackAsync(botClient, message, cancellationToken),
            State.WaitingForSelectProfession => HandleProfessionAsync(botClient, message, cancellationToken),
            State.WaitingForSelectSettings => HandleSelectedSettingsAsync(botClient, message, cancellationToken),
            State.WaitingForSelectInvestmentMenu => HandleSelectedInvestmentMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectEntrepreneurshipMenu => HandleSelectedEntrepreneurshipMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectProjectManagementMenu => HandleSelectedProjectManagementMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectRepresentationMenu => HandleSelectedRepresentationMenuAsync(botClient, message, cancellationToken),
            State.WaitingForFeedbackForOrganization => HandleFeedbackForOrganizationAsync(botClient, message, cancellationToken),
            State.WaitingForFeedbackForTelegramBot => HandleFeedbackForTelegramBotAsync(botClient, message, cancellationToken),
            State.WaitingForSelectPersonalInfo => HandleSelectedPersonalInfoAsync(botClient, message, cancellationToken),
            State.WaitingForSubmitApplication => SendGreetingAsync(botClient, message, cancellationToken),
            State.WaitingForResendApplication => HandleResendApplicationAsync(botClient, message, cancellationToken),
            State.WaitingForEnterFirstName => HandleFirstNameAsync(botClient, message, cancellationToken),
            State.WaitingForEnterLastName => HandleLastNameAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPatronomyc => HandlePatronomycAsync(botClient, message, cancellationToken),
            State.WaitingForEnterAge => HandleAgeAsync(botClient, message, cancellationToken),
            State.WaitingForEnterDateOfBirth => HandleDateOfBirthAsync(botClient, message, cancellationToken),
            State.WaitingForEnterDegree => HandleDegreeAsync(botClient, message, cancellationToken),
            State.WaitingForEnterEmail => HandleEmailAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPhoneNumber => HandlePhoneNumbeFromTextAsync(botClient, message, cancellationToken),
            State.WaitingForSelectLanguage => HandleSentLanguageAsync(botClient, message, cancellationToken),
            State.WaitingForEnterLanguages => HandleLanguagesAsync(botClient, message, cancellationToken),
            State.WaitingForEnterExperience => HandleExperienceAsync(botClient, message, cancellationToken),
            State.WaitingForEnterAddress => HandleAddressAsync(botClient, message, cancellationToken),
            State.WaitingForEnterArea => HandleAreaAsync(botClient, message, cancellationToken),
            State.WaitingForEnterInvestmentAmount => HandleInvestmentAmountAsync(botClient, message, cancellationToken),
            State.WaitingForEnterExpectation => HandleExpectationAsync(botClient, message, cancellationToken),
            State.WaitingForEnterAboutProject => HandleAboutProjectForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForEnterHelpType => HandleAboutHelpTypeForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForEnterRequiredFunding => HandleRequiredFundingForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForAssetInvested => HandleAssetsInvestedForEntrepreneurshipAsync(botClient, message, cancellationToken),
            State.WaitingForEnterProjectDirection => HandleProjectDirectionForProjectManagementAsync(botClient, message, cancellationToken),
            State.WaitingForEnterSector => HandleSectorFromTextAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPurpose => HandlePurposeAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleFirstNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if(message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendRequestForLastNameAsync(botClient, message, cancellationToken)
            };
            user.FirstName = message.Text; // TODO: need validations
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleLastNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendRequestForAgeAsync(botClient, message, cancellationToken)
            };
            user.LastName = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandlePatronomycAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text == localizer["rbtnCancel"])
        {
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            user.Patronomyc = message.Text; // TODO: need validation
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendRequestForDateOfBirthAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleAgeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            user.Age = message.Text; // TODO: need validation
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
                _ => SendRequestForDegreeAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleDateOfBirthAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
                user.DateOfBirth = dateOfBirth.ToUniversalTime();
                handler = user.Profession switch
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

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Investor => SendRequestForSectorAsync(botClient, message, cancellationToken),
                UserProfession.Entrepreneur => SendRequestForExperienceAsync(botClient, message, cancellationToken),
                _ => SendRequestForLanguagesAsync(botClient, message, cancellationToken),
            };
            user.Degree = message.Text.TrimStart('✅').Trim(); // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandlePhoneNumbeFromTextAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
                _ => SendRequestForEmailAsync(botClient, message, cancellationToken)
            };
            user.Phone = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleEmailAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
                _ => SendForSubmitApplicationAsync(botClient, message, cancellationToken)
            };
            user.Email = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleLanguagesAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendRequestForExperienceAsync(botClient, message, cancellationToken)
            };
            user.Languages = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleExperienceAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Entrepreneur => SendRequestForSectorAsync(botClient, message, cancellationToken),
                _ => SendRequestForAddressAsync(botClient, message, cancellationToken), // Works for PM and Representative
            };
            user.Experience = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleAddressAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                UserProfession.Representative => SendRequestForAreaAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendRequestForSectorAsync(botClient, message, cancellationToken),
                _ => HandleUnknownMessageAsync(botClient, message, cancellationToken),
            };
            user.Address = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleSectorFromTextAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);
        Task handler;

        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
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
            handler = user.Profession switch
            {
                UserProfession.Investor => SendRequestForInvestmentAmountForInvestmentAsync(botClient, message, cancellationToken),
                UserProfession.ProjectManager => SendRequestForExpectationAsync(botClient, message, cancellationToken),
                UserProfession.Entrepreneur => SendRequestForAboutProjectForEntrepreneurshipAsync(botClient, message, cancellationToken),
                _ => HandleUnknownMessageAsync(botClient, message, cancellationToken),
            };
            user.Sector = message.Text;
        }
        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}