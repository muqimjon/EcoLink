using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendRequestForAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var area = await mediator.Send(new GetRepresentativeAreaByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(area) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qayerda vakil bo'lmoqchisiz:",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(area) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterArea), cancellationToken);
    }

    private async Task SendRequestForExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var expectatiopn = string.Empty;
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var handle = profession switch
        {
            UserProfession.Representative => mediator.Send(new GetRepresentativeExpectationByUserIdQuery(user.Id), cancellationToken),
            UserProfession.ProjectManager => mediator.Send(new GetProjectManagerExpectationByUserIdQuery(user.Id), cancellationToken),
        };
        
        try { expectatiopn = await handle; }
        catch(Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
        
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
