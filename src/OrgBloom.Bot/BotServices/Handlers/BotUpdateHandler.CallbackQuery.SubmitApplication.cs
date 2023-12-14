using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task HandleSubmitApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var handle = callbackQuery.Data switch
        {
            "submitForInvestor" => SubmitInvestmentApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownSubmissionAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private Task HandleUnknownSubmissionAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task SubmitInvestmentApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nInvestorlik uchun murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        Thread.SpinWait(5000);
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }
}
