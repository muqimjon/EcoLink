using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Commons.Constants;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendApplyQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
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
            text: localizer["txtAskForApplication"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectProfession), cancellationToken);
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForResendApplication), cancellationToken);
    }

    private async Task SendForSubmitApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var applicationText = profession switch
        {
            UserProfession.ProjectManager => StringHelper.GetApplicationInfoForm(await mediator.Send(new GetProjectManagerByUserIdQuery(user.Id), cancellationToken)),
            UserProfession.Investor => StringHelper.GetApplicationInfoForm(await mediator.Send(new GetInvestorByUserIdQuery(user.Id), cancellationToken)),
            UserProfession.Entrepreneur => StringHelper.GetApplicationInfoForm(await mediator.Send(new GetEntrepreneurByUserIdQuery(user.Id), cancellationToken)),
            UserProfession.Representative => StringHelper.GetApplicationInfoForm(await mediator.Send(new GetRepresentativeByUserIdQuery(user.Id), cancellationToken)),
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSubmitApplication), cancellationToken);
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterFirstName), cancellationToken);
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterLastName), cancellationToken);
    }

    private async Task SendRequestForPatronomycAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var exist = await mediator.Send(new GetUserByIdQuery() { Id = user.Id }, cancellationToken);
        var args = string.IsNullOrEmpty(exist.Patronomyc) switch
        {
            true => (localizer["txtAskForPatronomyc"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForPatronomyc"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(exist.Patronomyc)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterPatronomyc), cancellationToken);
    }

    private async Task SendRequestForDateOfBirthAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var dateOfBirth = await mediator.Send(new GetDateOfBirthQuery(user.Id), cancellationToken);
        var formattedDate = dateOfBirth.ToString().Split().First();
        var @default = DateTimeOffset.MinValue.AddHours(TimeConstants.UTC);

        var args = (dateOfBirth == @default) switch
        {
            true => (localizer["txtAskForDateOfBirth"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForDateOfBirth"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(formattedDate)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterDateOfBirth), cancellationToken);
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterDegree), cancellationToken);
    }

    private async Task SendRequestForLanguagesAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var languages = await mediator.Send(new GetLanguagesQuery(user.Id), cancellationToken);
        var args = string.IsNullOrEmpty(languages) switch
        {
            true => (localizer["txtAskForSkillLanguages"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForSkillLanguages"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(languages)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterLanguages), cancellationToken);
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterPhoneNumber), cancellationToken);
    }

    private async Task SendRequestForEmailAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var email = await mediator.Send(new GetEmailQuery(user.Id), cancellationToken);
        var args = string.IsNullOrEmpty(email) switch
        {
            true => (localizer["txtAskForEmail"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForEmail"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(email)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterEmail), cancellationToken);
    }

    private async Task SendRequestForExperienceAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var experience = await mediator.Send(new GetExperienceQuery(user.Id), cancellationToken);
        var args = string.IsNullOrEmpty(experience) switch
        {
            true => (localizer["txtAskForExperience"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForExperience"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(experience)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterExperience), cancellationToken);
    }

    private async Task SendRequestForAddressAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var address = await mediator.Send(new GetAddressQuery(user.Id), cancellationToken);
        var args = string.IsNullOrEmpty(address) switch
        {
            true => (localizer["txtAskForAddress"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForAddress"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(address)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterAddress), cancellationToken);
    }

    private async Task SendRequestForExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var expectatiopn = string.Empty;
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var handle = profession switch
        {
            UserProfession.Representative => mediator.Send(new GetRepresentativeExpectationByUserIdQuery(user.Id), cancellationToken),
            UserProfession.ProjectManager => mediator.Send(new GetProjectManagerExpectationByUserIdQuery(user.Id), cancellationToken),
            UserProfession.Entrepreneur => throw new NotImplementedException(),
            UserProfession.Investor => throw new NotImplementedException(),
            _ => Task.FromResult(string.Empty)
        };

        try { expectatiopn = await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        var args = string.IsNullOrEmpty(expectatiopn) switch
        {
            true => (localizer["txtAskForExpectation"], new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (localizer["txtAskForExpectation"] + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(expectatiopn)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterExpectation), cancellationToken);
    }

    private async Task SendRequestForPurposeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var purpose = string.Empty;
        var askPurpose = string.Empty;
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);

        switch(profession)
        {
            case UserProfession.Representative:
                purpose = await mediator.Send(new GetRepresentativePurposeByUserIdQuery(user.Id), cancellationToken);
                askPurpose = localizer["txtAskForRepresentativePurpose"];
                break;
            case UserProfession.ProjectManager:
                purpose = await mediator.Send(new GetProjectManagerPurposeByUserIdQuery(user.Id), cancellationToken);
                askPurpose = localizer["txtAskForProjectManagerPurpose"];
                break;
            default:
                throw new NotImplementedException();
        };

        var args = string.IsNullOrEmpty(purpose) switch
        {
            true => (askPurpose, new ReplyKeyboardMarkup(new[] { new KeyboardButton(localizer["rbtnCancel"]) }) { ResizeKeyboard = true }),
            false => (askPurpose + localizer["txtAskWithButton"], new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(purpose)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true })
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: args.Item1,
            replyMarkup: args.Item2,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterPurpose), cancellationToken);
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterSector), cancellationToken);
    }

    private async Task SendProfessionInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: "Professia haqida",
            cancellationToken: cancellationToken);
    }
}
