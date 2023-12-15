using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task SendApplyQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
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
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(user.FirstName) }
        })
        { ResizeKeyboard = true };

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
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(user.LastName) }
        })
        { ResizeKeyboard = true };

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
                    new[] { new KeyboardButton(exist.Patronomyc) }
                })
                { ResizeKeyboard = true });

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
                    new[] { new KeyboardButton(formattedDate) }
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

    private async Task SendRequestForPhoneNumberAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(
            new[] { new KeyboardButton("Telefon raqamni jo'natish") { RequestContact = true } } )
        { ResizeKeyboard = true };

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
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(email) }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Emailingizni kiriting: ",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(email) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterEmail), cancellationToken);
    }
}
