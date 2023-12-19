using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private Task SendRepresentationMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task SendRequestForAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var area = await mediator.Send(new GetRepresentativeAreaByUserIdQuery(user.Id), cancellationToken);
        var replyKeyboard = string.IsNullOrEmpty(area) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(area)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForArea"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterArea), cancellationToken);
    }
}
