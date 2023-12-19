using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendGreetingAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var isUserNew = await mediator.Send(new IsUserNewQuery(user.Id), cancellationToken);
        if (isUserNew)
        {
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
                [InlineKeyboardButton.WithCallbackData("🇺🇿 o'zbekcha 🇺🇿", "ibtnUz")],
                [InlineKeyboardButton.WithCallbackData("🇬🇧 english 🇬🇧", "ibtnEn")],
                [InlineKeyboardButton.WithCallbackData("🇷🇺 русский 🇷🇺", "ibtnRu")] });

            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: localizer["greeting", user.FirstName, user.LastName],
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);

            await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectLanguage), cancellationToken);
        }
        else await SendMainMenuAsync(botClient, message, cancellationToken);
    }

    public async Task SendMainMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnDepartaments"])],
            [new(localizer["rbtnContact"]), new(localizer["rbtnFeedback"])],
            [new(localizer["rbtnSettings"]), new(localizer["rbtnInfo"]),]
        }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMainMenu"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectMainMenu), cancellationToken);
    }

    private async Task SendInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtOrganizationInfo"],
            cancellationToken: cancellationToken
        );
    }

    private async Task SendSettingsQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup keyboard = new(new KeyboardButton[][]
        {
            [new(localizer["rbtnEditLanguage"])],
            [new(localizer["rbtnEditPersonalInfo"])],
            [new(localizer["rbtnBack"])]
        }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtSettings"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectSettings), cancellationToken);
    }

    private async Task SendFeedbackQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnFeedbackForOrganization"]), new(localizer["rbtnFeedbackForTelegramBot"])],
            [new(localizer["rbtnBack"])]
        }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForFeedback"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectForFeedback), cancellationToken);
    }

    private async Task SendSelectLanguageQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnUzbek"])],
            [new(localizer["rbtnRussian"])],
            [new(localizer["rbtnEnglish"])],
            [new(localizer["rbtnBack"])]
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskForSelectLanguage"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectLanguage), cancellationToken);
    }

    private async Task SendEditPersonalInfoQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnPhoneNumber"]), new(localizer["rbtnEmail"])],
            [new(localizer["rbtnBack"])]
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtEditPersonalInfo"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectPersonalInfo), cancellationToken);
    }

    private async Task SendContactAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtContactInfo"],
            cancellationToken: cancellationToken
        );
    }

    private async Task SendRequestFeedbackForTelegramBotQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[]{new(localizer["rbtnCancel"])}) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskFeedbackFoTelegramBot"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForFeedbackForTelegramBot), cancellationToken);
    }

    private async Task SendRequestFeedbackForOrganizationQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[] { new(localizer["rbtnCancel"]) }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskFeedbackForOrganization"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForFeedbackForOrganization), cancellationToken);
    }
}
