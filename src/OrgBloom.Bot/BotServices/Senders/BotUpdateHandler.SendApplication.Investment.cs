using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendRequestForInvestmentAmountForInvestmentAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var investmrntAmount = await mediator.Send(new GetInvestmentAmountByUserIdQuery(user.Id));
        var replyKeyboard = string.IsNullOrEmpty(investmrntAmount) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnBack"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(investmrntAmount)], [new(localizer["rbtnBack"])] }) { ResizeKeyboard = true },
        };

        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: localizer["txtAskForInvestmentAmount"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterInvestmentAmount), cancellationToken);
    }
}
