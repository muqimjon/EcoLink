namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendMenuRepresentationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnApply"]), new(localizer["rbtnInfo"]),],
            [new(localizer["rbtnBack"])],
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMenuRepresentation"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.Profession = UserProfession.Representative;
        user.State = State.WaitingForSelectRepresentationMenu;
    }

    private async Task SendRequestForAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        string area = user.Profession switch
        {
            UserProfession.Representative => user.Representation.Area,
            UserProfession.ProjectManager => user.ProjectManagement.Area,
            _ => default!
        };

        var replyKeyboard = string.IsNullOrEmpty(area) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(area)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true }
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForArea"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard);

        user.State = State.WaitingForEnterArea;
    }
}
