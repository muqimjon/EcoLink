using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Bot.BotServices.Helpers;
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
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton("Investitsiya jalb qilish") },
            new[] { new KeyboardButton("Investorlik qilish") },
            new[] { new KeyboardButton("Vakil bo'lish") },
            new[] { new KeyboardButton("Loyiha boshqarish") }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Nima maqsadda ariza topshirmoqchisiz?",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectProfession), cancellationToken);
    }

    private async Task SendAlreadyExistApplicationAsync(string text, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton("Qayta yuborish") },
            new[] { new KeyboardButton("Ortga") }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Sizda Tastiqlangan murojaat mavjud\n" + text,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForResendApplication), cancellationToken);
    }

    private async Task SendRequestForFirstNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(user.FirstName) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Ismingizni kiriting: ",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterFirstName), cancellationToken);
    }

    private async Task SendRequestForLastNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(user.LastName) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Familiyangizni kiriting: ",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterLastName), cancellationToken);
    }

    private async Task SendRequestForPatronomycAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var exist = await mediator.Send(new GetUserByIdQuery() { Id = user.Id }, cancellationToken);
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Otangizning ismi: ",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(exist.Patronomyc) ?
                new ReplyKeyboardRemove() :
                new ReplyKeyboardMarkup(new[] 
                { 
                    new KeyboardButton(exist.Patronomyc) 
                }) { ResizeKeyboard = true });

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterPatronomyc), cancellationToken);
    }

    private async Task SendRequestForDateOfBirthAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var exist = await mediator.Send(new GetUserByIdQuery() { Id = user.Id }, cancellationToken);
        var formattedDate = exist.DateOfBirth.ToString()!.Split().First();
        var isWithinRange = exist.DateOfBirth >= new DateTimeOffset(1950, 1, 1, 0, 0, 0, TimeSpan.Zero)
                            && exist.DateOfBirth <= new DateTimeOffset(2005, 12, 31, 23, 59, 59, TimeSpan.Zero);

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Tug'ilgan sana: (kk.oo.yyyy)",
            cancellationToken: cancellationToken,
            replyMarkup: isWithinRange ?
                new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton(formattedDate)
                }) { ResizeKeyboard = true } :
                new ReplyKeyboardRemove());

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterDateOfBirth), cancellationToken);
    }

    private async Task SendRequestForDegreeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton("O'rta"), new KeyboardButton("O'rta maxsusus") },
            new[] { new KeyboardButton("Oliy"), new KeyboardButton("Magistr") }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Ma'lumotingiz: ",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterDegree), cancellationToken);
    }

    private async Task SendRequestForLanguagesAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var languages = await mediator.Send(new GetLanguagesQuery(user.Id), cancellationToken);

        var replyKeyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(languages) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qaysi tillarni bilasiz: ",
            replyMarkup: string.IsNullOrEmpty(languages) ? new ReplyKeyboardRemove() : replyKeyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterLanguages), cancellationToken);
    }

    private async Task SendRequestForPhoneNumberAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup( new[] { new KeyboardButton("Telefon raqamni jo'natish") { RequestContact = true } } ) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Telefon raqamingizni kiriting: ",
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterPhoneNumber), cancellationToken);
    }

    private async Task SendRequestForEmailAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var email = await mediator.Send(new GetEmailQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(email) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Emailingizni kiriting: ",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(email) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterEmail), cancellationToken);
    }

    private async Task SendRequestForExperienceAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var experience = await mediator.Send(new GetExperienceQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(experience) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qaysi sohada necha yillik tajribagiz bor:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(experience) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterExperience), cancellationToken);
    }

    private async Task SendRequestForAddressAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var address = await mediator.Send(new GetAddressQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(address) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qayerda yashaysiz:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(address) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterAddress), cancellationToken);
    }

    private async Task SendForSubmitApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var applicationText = profession switch
        {
            UserProfession.ProjectManager => StringHelper.GetProjectManagementApplicationInfoForm(await mediator.Send(new GetProjectManagerByUserIdQuery(user.Id), cancellationToken)),
            UserProfession.Investor => StringHelper.GetInvestmentApplicationInfoForm(await mediator.Send(new GetInvestorByUserIdQuery(user.Id), cancellationToken)),
            UserProfession.Entrepreneur => StringHelper.GetEntrepreneurshipApplicationInfoForm(await mediator.Send(new GetEntrepreneurByUserIdQuery(user.Id), cancellationToken)),
            UserProfession.Representative => StringHelper.GetRepresentationApplicationInfoForm(await mediator.Send(new GetRepresentativeByUserIdQuery(user.Id), cancellationToken)),
            _ => string.Empty,
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Murojaatnoma tayyor tasdiqlash qoldi xolos!",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken
        );

        await Task.Delay(1000, cancellationToken);
        var keyboard = new InlineKeyboardMarkup(new[] {
            new[] { InlineKeyboardButton.WithCallbackData("Tasdiqlash", "submit") },
            new[] { InlineKeyboardButton.WithCallbackData("E'tiborsiz qoldrish", "cancel") }
        });

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: applicationText,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSubmitApplication), cancellationToken);
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

        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(expectatiopn) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Iqtisodiyot Assambleyadan nima kutasiz:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(expectatiopn) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterExpectation), cancellationToken);
    }

    private async Task SendRequestForPurposeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var purpose = string.Empty;
        var professionName = string.Empty;
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        switch(profession)
        {
            case UserProfession.Representative:
                purpose = await mediator.Send(new GetRepresentativePurposeByUserIdQuery(user.Id), cancellationToken);
                professionName = "Vakil bo'lish";
                break;
            case UserProfession.ProjectManager:
                purpose = await mediator.Send(new GetProjectManagerPurposeByUserIdQuery(user.Id), cancellationToken);
                professionName = "Loyiha boshqarish";
                break;
            default:
                throw new NotImplementedException();
        };

        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(purpose) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: professionName + "dan maqsadingiz:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(purpose) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterPurpose), cancellationToken);
    }
}
