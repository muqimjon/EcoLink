using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendRequestForInvestmentAmountForInvestmentAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: localizer["txtAskForInvestmentAmount"],
            cancellationToken: cancellationToken,
            replyMarkup: new ReplyKeyboardRemove());

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterInvestmentAmount), cancellationToken);
    }
}
