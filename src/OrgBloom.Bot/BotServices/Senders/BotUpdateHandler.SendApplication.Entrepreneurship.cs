using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendEntrepreneurshipMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnApply"]), new(localizer["rbtnInfo"]),],
            [new(localizer["rbtnBack"])],
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtEntrepreneurshipMenu"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectEntrepreneurshipMenu), cancellationToken);
    }

    private async Task SendRequestForAboutProjectForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var project = await mediator.Send(new GetEntrepreneurProjectByUserIdQuery(user.Id), cancellationToken);
        var replyKeyboard = string.IsNullOrEmpty(project) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(project)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForAboutProject"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterAboutProject), cancellationToken);
    }

    private async Task SendRequestForHelpTypeEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var helpType = await mediator.Send(new GetEntrepreneurHelpTypeByUserIdQuery(user.Id), cancellationToken);
        var replyKeyboard = string.IsNullOrEmpty(helpType) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(helpType)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForHelpType"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterHelpType), cancellationToken);
    }

    private async Task SendRequestForRequiredFundingForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var requiredFunding = await mediator.Send(new GetEntrepreneurRequiredFundingByUserIdQuery(user.Id), cancellationToken);
        var replyKeyboard = string.IsNullOrEmpty(requiredFunding) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(requiredFunding)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForRequiredFunding"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterRequiredFunding), cancellationToken);
    }

    private async Task SendRequestForAssetsInvestedForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var assetsInvested = await mediator.Send(new GetEntrepreneurAssetsInvestedByUserIdQuery(user.Id), cancellationToken);
        var replyKeyboard = string.IsNullOrEmpty(assetsInvested) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(assetsInvested)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForAssetInvested"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForAssetInvested), cancellationToken);
    }
}
