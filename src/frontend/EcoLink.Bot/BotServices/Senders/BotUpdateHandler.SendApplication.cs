using EcoLink.ApiService.Constants;
using EcoLink.ApiService.Models.Investment;
using EcoLink.ApiService.Models.Representation;
using EcoLink.ApiService.Models.Entrepreneurship;
using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendMenuProfessionsAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnEntrepreneurship"])],
            [new(localizer["rbtnInvestment"])],
            [new(localizer["rbtnRepresentation"])],
            [new(localizer["rbtnProjectManagement"])],
            [new(localizer["rbtnBack"])]
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMenuDepartaments"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.Profession = UserProfession.None;
        user.State = State.WaitingForSelectProfession;
    }

    private async Task SendAlreadyExistApplicationAsync(string text, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(localizer["rbtnResend"])], [new(localizer["rbtnBack"])] }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtSubmittedApplication"] + text,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.State = State.WaitingForResendApplication;
    }

    private async Task SendForSubmitApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var sending = await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtReadyApplication"],
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken
        );

        await Task.Delay(2000, cancellationToken);

        var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
        {
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnSubmit"], "submit")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnCancel"], "cancel")]
        });

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: GetApplicationInForm(),
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.MessageId = sending.MessageId;
        user.State = State.WaitingForSubmitApplication;
    }

    private async Task SendRequestForFirstNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.FirstName) switch
        {
            true => (localizer["txtAskForFirstName"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForFirstName"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.FirstName)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken
        );

        user.State = State.WaitingForEnterFirstName;
    }

    private async Task SendRequestForLastNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.LastName) switch
        {
            true => (localizer["txtAskForLastName"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForLastName"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.LastName)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken
        );

        user.State = State.WaitingForEnterLastName;
    }

    private async Task SendRequestForPatronomycAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.Patronomyc) switch
        {
            true => (localizer["txtAskForPatronomyc"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForPatronomyc"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Patronomyc)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterPatronomyc;
    }

    private async Task SendRequestForAgeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.Age) switch
        {
            true => (localizer["txtAskForAge"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForAge"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Age)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterAge;
    }

    private async Task SendRequestForDateOfBirthAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var formattedDate = user.DateOfBirth.ToString("dd.MM.yyyy");
        var @default = DateTimeOffset.MinValue.AddHours(TimeConstants.UTC);

        var args = (user.DateOfBirth == @default) switch
        {
            true => (localizer["txtAskForDateOfBirth"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForDateOfBirth"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(formattedDate)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterDateOfBirth;
    }

    private async Task SendRequestForDegreeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnUndergraduateDegree"]), new (localizer["rbtnSpecialistDegree"])],
            [new (localizer["rbtnBachelorDegree"]), new (localizer["rbtnMagistrDegree"])],
            [new (localizer["rbtnCancel"])],
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForDegree"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.State = State.WaitingForEnterDegree;
    }

    private async Task SendRequestForLanguagesAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.Languages) switch
        {
            true => (localizer["txtAskForSkillLanguages"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForSkillLanguages"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Languages)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken
        );

        user.State = State.WaitingForEnterLanguages;
    }

    private async Task SendRequestForPhoneNumberAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup( new KeyboardButton[][] 
        { 
            [new(localizer["rbtnSendContact"]) { RequestContact = true }], 
            [new(localizer["rbtnCancel"])] 
        }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForPhoneNumber"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken
        );

        user.State = State.WaitingForEnterPhoneNumber;
    }

    private async Task SendRequestForEmailAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.Email) switch
        {
            true => (localizer["txtAskForEmail"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForEmail"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Email)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterEmail;
    }

    private async Task SendRequestForExperienceAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.Experience) switch
        {
            true => (localizer["txtAskForExperience"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForExperience"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Experience)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterExperience;
    }

    private async Task SendRequestForAddressAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.Address) switch
        {
            true => (localizer["txtAskForAddress"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForAddress"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Address)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterAddress;
    }

    private async Task SendRequestForExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var args = string.IsNullOrEmpty(user.Expectation) switch
        {
            true => (localizer["txtAskForExpectation"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForExpectation"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Expectation)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterExpectation;
    }

    private async Task SendRequestForPurposeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        string askPurpose = user.Profession switch
        {
            UserProfession.Representative => localizer["txtAskForRepresentativePurpose"],
            UserProfession.ProjectManager => localizer["txtAskForProjectManagerPurpose"],
            _ => string.Empty
        };

        var args = string.IsNullOrEmpty(user.Purpose) switch
        {
            true => (askPurpose, new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (askPurpose + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Purpose)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        user.State = State.WaitingForEnterPurpose;
    }
    
    private async Task SendRequestForSectorAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
        {
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnIT"], "sectorIT")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnManufacturing"], "sectorManufacturing")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnTrade"], "sectorTrade")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnConstruction"], "sectorConstruction")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnAgriculture"], "sectorAgriculture")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnEnergy"], "sectorEnergy")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnEducation"], "sectorEducation")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnFranchise"], "sectorFranchise")]
        });

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForSector"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken);

        var sending = await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForSelectOrWrite"],
            replyMarkup: new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            cancellationToken: cancellationToken);

        user.MessageId = sending.MessageId;
        user.State = State.WaitingForEnterSector;
    }

    private async Task SendProfessionInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = user.Profession switch
        {
            UserProfession.Investor => botClient.SendTextMessageAsync(chatId: message.Chat.Id,text: localizer["txtInfoInvestment"],cancellationToken: cancellationToken),
            UserProfession.Entrepreneur => botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: localizer["txtInfoEntrepreneurship"], cancellationToken: cancellationToken),
            UserProfession.Representative => botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: localizer["txtInfoRepresentation"], cancellationToken: cancellationToken),
            UserProfession.ProjectManager => botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: localizer["txtInfoProjectManagement"], cancellationToken: cancellationToken),
            _ => throw new NotImplementedException()
        };

        try { await handler; }
        catch(Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private string GetApplicationInForm()
    {
        return user.Profession switch
        {
            UserProfession.Entrepreneur => localizer["txtApplicationEntrepreneurship",
                user.FirstName,
                user.LastName,
                user.Age,
                user.Degree,
                user.Experience,
                user.Project,
                user.HelpType,
                user.RequiredFunding,
                user.AssetsInvested,
                user.Phone,
                user.Email,
                user.Profession],
            UserProfession.Investor => localizer["txtApplicationInvestment",
                user.FirstName,
                user.LastName,
                user.Age,
                user.Degree,
                user.Sector,
                user.InvestmentAmount,
                user.Phone,
                user.Email,
                user.Profession],
            UserProfession.Representative => localizer["txtApplicationRepresentation",
                user.FirstName,
                user.LastName,
                user.Age,
                user.Degree,
                user.Languages,
                user.Experience,
                user.Address,
                user.Area,
                user.Expectation,
                user.Purpose,
                user.Phone,
                user.Email,
                user.Profession],
            UserProfession.ProjectManager => localizer["txtApplicationProjectManagement",
                user.FirstName,
                user.LastName,
                user.Age,
                user.Degree,
                user.Languages,
                user.Experience,
                user.Address,
                user.Sector,
                user.Expectation,
                user.Purpose,
                user.Phone,
                user.Email,
                user.Profession],
            _ => string.Empty,
        };
    }

    private string GetApplicationInForm(ProjectManagementAppDto dto)
        => localizer["txtApplicationProjectManagement",
                dto.FirstName,
                dto.LastName,
                dto.Age,
                dto.Degree,
                dto.Languages,
                dto.Experience,
                dto.Address,
                dto.Sector,
                dto.Expectation,
                dto.Purpose,
                dto.Phone,
                dto.Email,
                user.Profession];

    private string GetApplicationInForm(EntrepreneurshipAppDto dto)
        => localizer["txtApplicationEntrepreneurship",
                dto.FirstName,
                dto.LastName,
                dto.Age,
                dto.Degree,
                dto.Experience,
                dto.Project,
                dto.HelpType,
                dto.RequiredFunding,
                dto.AssetsInvested,
                dto.Phone,
                dto.Email,
                user.Profession];

    private string GetApplicationInForm(InvestmentAppDto dto)
        => localizer["txtApplicationInvestment",
                dto.FirstName,
                dto.LastName,
                dto.Age,
                dto.Degree,
                dto.Sector,
                dto.InvestmentAmount,
                dto.Phone,
                dto.Email,
                user.Profession];

    private string GetApplicationInForm(RepresentationAppDto dto)
        => localizer["txtApplicationRepresentation",
                dto.FirstName,
                dto.LastName,
                dto.Age,
                dto.Degree,
                dto.Languages,
                dto.Experience,
                dto.Address,
                dto.Area,
                dto.Expectation,
                dto.Purpose,
                dto.Phone,
                dto.Email,
                user.Profession];
}
