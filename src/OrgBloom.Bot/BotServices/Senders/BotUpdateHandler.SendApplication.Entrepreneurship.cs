using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendRequestForAboutProjectForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var project = await mediator.Send(new GetEntrepreneurProjectByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(project) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForAboutProject"],
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(project) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterAboutProject), cancellationToken);
    }

    private async Task SendRequestForHelpTypeEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var helpType = await mediator.Send(new GetEntrepreneurHelpTypeByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(helpType) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForHelpType"],
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(helpType) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterHelpType), cancellationToken);
    }

    private async Task SendRequestForRequiredFundingForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var requiredFunding = await mediator.Send(new GetEntrepreneurRequiredFundingByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(requiredFunding) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForRequiredFunding"],
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(requiredFunding) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterRequiredFunding), cancellationToken);
    }

    private async Task SendRequestForAssetsInvestedForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var assetsInvested = await mediator.Send(new GetEntrepreneurAssetsInvestedByUserIdQuery(user.Id), cancellationToken);
        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton(assetsInvested) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForAssetInvested"],
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(assetsInvested) ? new ReplyKeyboardRemove() : keyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForAssetInvested), cancellationToken);
    }
}
