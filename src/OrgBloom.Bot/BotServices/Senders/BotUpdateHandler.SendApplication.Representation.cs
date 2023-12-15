using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendRequestForAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var area = await mediator.Send(new GetRepresentativeAreaByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(area) }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qayerda vakil bo'lmoqchisiz:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(area) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterArea), cancellationToken);
    }

    private async Task SendRequestForExpectationForRepresentationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var expectatiopn = await mediator.Send(new GetRepresentativeExpectationByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(expectatiopn) }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Iqtisodiyot Assambleyadan nima kutasiz:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(expectatiopn) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterExpectation), cancellationToken);
    }
}
