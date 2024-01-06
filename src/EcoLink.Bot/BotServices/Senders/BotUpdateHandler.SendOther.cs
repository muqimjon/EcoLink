namespace EcoLink.Bot.BotServices;

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
        await botClient.SendPhotoAsync(
            chatId: message.Chat.Id,
            photo: InputFile.FromUri("https://media.licdn.com/dms/image/D4E16AQG21-Nxg6zYAg/profile-displaybackgroundimage-shrink_350_1400/0/1678436459958?e=1708560000&v=beta&t=tyv-TnDHrW5cK3Q7b7Uu8Ch4nRVNA0KdAAGCqrdoBZg"),
            caption: localizer["txtOrganizationInfo"],
            cancellationToken: cancellationToken);
    }

    private async Task SendMenuSettingsAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
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

    private async Task SendFeedbackMenuQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnFeedbackForOrganization"]), new(localizer["rbtnFeedbackForTelegramBot"])],
            [new(localizer["rbtnBack"])]
        }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMenuFeedback"],
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
            text: localizer["txtMenuLanguage"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectLanguage), cancellationToken);
    }

    private async Task SendMenuEditPersonalInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
            [new(localizer["rbtnPhoneNumber"]), new(localizer["rbtnEmail"])],
            [new(localizer["rbtnBack"])]
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMenuPersonalInfo"],
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

        await botClient.SendLocationAsync(
            chatId: message.Chat.Id,
            latitude: 41.31255776545841,
            longitude: 69.24048566441775,
            cancellationToken: cancellationToken);
    }

    private async Task SendRequestFeedbackForTelegramBotAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[]{new(localizer["rbtnCancel"])}) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtAskFeedbackFoTelegramBot"],
            replyMarkup: replyKeyboard,
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForFeedbackForTelegramBot), cancellationToken);
    }

    private async Task SendRequestFeedbackForOrganizationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
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
