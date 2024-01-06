namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendMenuProjectManagementAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnApply"]), new(localizer["rbtnInfo"]),],
            [new(localizer["rbtnBack"])],
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMenuProjectManagement"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateAndProfessionCommand()
        {
            Id = user.Id,
            Profession = UserProfession.ProjectManager,
            State = State.WaitingForSelectProjectManagementMenu,
        }, cancellationToken: cancellationToken);
    }
}
