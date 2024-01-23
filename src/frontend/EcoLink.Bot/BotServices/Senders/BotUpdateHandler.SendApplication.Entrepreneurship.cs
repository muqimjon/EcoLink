namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendMenuEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnApply"]), new(localizer["rbtnInfo"]),],
            [new(localizer["rbtnBack"])],
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMenuEntrepreneurship"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );


        user.Profession = UserProfession.Entrepreneur;
        user.State = State.WaitingForSelectEntrepreneurshipMenu;
    }

    private async Task SendRequestForAboutProjectForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = string.IsNullOrEmpty(user.Application.Project) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Application.Project)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
            _ => default
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForAboutProject"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        user.State = State.WaitingForEnterAboutProject;
    }

    private async Task SendRequestForHelpTypeEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = string.IsNullOrEmpty(user.Application.HelpType) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Application.HelpType)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
            _ => default
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForHelpType"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        user.State = State.WaitingForEnterHelpType;
    }

    private async Task SendRequestForRequiredFundingForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = string.IsNullOrEmpty(user.Application.RequiredFunding) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Application.RequiredFunding)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
            _ => default
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForRequiredFunding"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        user.State = State.WaitingForEnterRequiredFunding;
    }

    private async Task SendRequestForAssetsInvestedForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = string.IsNullOrEmpty(user.Application.AssetsInvested) switch
        {
            true => new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true },
            false => new ReplyKeyboardMarkup(new KeyboardButton[][] { [new(user.Application.AssetsInvested)], [new(localizer["rbtnCancel"])] }) { ResizeKeyboard = true },
            _ => default
        };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForAssetInvested"],
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboard
        );

        user.State = State.WaitingForAssetInvested;
    }
}
