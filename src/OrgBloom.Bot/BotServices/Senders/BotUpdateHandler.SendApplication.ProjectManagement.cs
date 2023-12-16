using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendRequestForProjectDirectionAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var projectDirection = await mediator.Send(new GetProjectManagerProjectDirectionByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(projectDirection) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qaysi sohada loyiha boshqarmoqchisiz:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(projectDirection) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterProjectDirection), cancellationToken);
    }
}
