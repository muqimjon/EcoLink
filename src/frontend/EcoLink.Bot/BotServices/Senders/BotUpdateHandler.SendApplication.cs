using EcoLink.ApiService.Constants;

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
    }

    private async Task SendForSubmitApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var applicationText = user.Profession switch
        {
            UserProfession.Entrepreneur => localizer["txtApplicationEntrepreneurship", 
                user.FirstName, 
                user.LastName,
                user.Age,
                user.Degree,
                user.Experience,
                (string)user.Application.Project,
                (string)user.Application.HelpType,
                (string)user.Application.RequiredFunding,
                (string)user.Application.AssetsInvested,
                user.Phone,
                user.Email],
            UserProfession.Investor => localizer["txtApplicationInvestment",
                user.FirstName,
                user.LastName,
                user.Age,
                user.Degree,
                (string)user.Application.Sector,
                (string)user.Application.InvestmentAmount,
                user.Phone,
                user.Email],
            UserProfession.Representative => localizer["txtApplicationRepresentation",
                user.FirstName,
                user.LastName,
                user.Age,
                user.Degree,
                user.Languages,
                user.Experience,
                user.Address,
                (string)user.Application.Area,
                (string)user.Application.Expectation,
                (string)user.Application.Purpose,
                user.Phone,
                user.Email],
            UserProfession.ProjectManager => localizer["txtApplicationProjectManagement",
                user.FirstName,
                user.LastName,
                user.Age,
                user.Degree,
                user.Languages,
                user.Experience,
                user.Address,
                (string)user.Application.ProjectDirection,
                (string)user.Application.Expectation,
                (string)user.Application.Purpose,
                user.Phone,
                user.Email],
            _ => string.Empty,
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtReadyApplication"],
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken
        );

        await Task.Delay(1000, cancellationToken);
        var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnSubmit"], "submit")],
            [InlineKeyboardButton.WithCallbackData(localizer["ibtnCancel"], "cancel")]
        });

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: applicationText,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

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
        var args = string.IsNullOrEmpty(user.Application.Expectation) switch
        {
            true => (localizer["txtAskForExpectation"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForExpectation"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Application.Expectation)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
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
        var askPurpose = user.Profession switch
        {
            UserProfession.Representative => localizer["txtAskForRepresentativePurpose"],
            UserProfession.ProjectManager => localizer["txtAskForProjectManagerPurpose"],
            _ => throw new NotImplementedException()
        };

        var args = string.IsNullOrEmpty(user.Application.Purpose) switch
        {
            true => (askPurpose, new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (askPurpose + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Application.Purpose)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true }),
            _ => default
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

        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: localizer["txtAskForSelectOrWrite"],
            replyMarkup: new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            cancellationToken: cancellationToken);

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
}
