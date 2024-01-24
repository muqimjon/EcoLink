namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendMenuInvestmentAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnApply"]), new(localizer["rbtnInfo"]),],
            [new(localizer["rbtnBack"])],
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMenuInvestment"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.Profession = UserProfession.Investor;
        user.State = State.WaitingForSelectInvestmentMenu;
    }

    private async Task SendRequestForInvestmentAmountForInvestmentAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        var replyKeyboard = string.IsNullOrEmpty(user.Investment.InvestmentAmount) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Investment.InvestmentAmount)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
        };

        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: localizer["txtAskForInvestmentAmount"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard);

        user.State = State.WaitingForEnterInvestmentAmount;
    }
}
