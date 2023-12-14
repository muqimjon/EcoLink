using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task HandleSubmittionApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var profession = callbackQuery.Data switch
        {
            "submit" => HandleSubmitApplicationAsync(botClient, callbackQuery, cancellationToken),
            "cancel" => HandleCancelApplication(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownSubmissionAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await profession; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }
    }

    private async Task HandleSubmitApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var handle = profession switch
        {
            UserProfession.Entrepreneur => HandleSubmitEntrepreneurApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Investor => HandleSubmitInvestmentApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.ProjectManager => HandleSubmitProjectManagerApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Representative => HandleSubmitRepresentativeApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownSubmissionAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private async Task HandleCancelApplication(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id, text: "Ma'lumotlaringiz saqlab qolinadi va qayta yubormoqchi bo'lsangiz bu sizga yordam beradi", cancellationToken: cancellationToken);
        Thread.Sleep(1000);
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private Task HandleUnknownSubmissionAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task HandleSubmitRepresentativeApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nVakillik qilish uchun murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        Thread.Sleep(1000);
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private async Task HandleSubmitProjectManagerApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nLoyiha boshqarish uchun murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        Thread.Sleep(1000);
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private async Task HandleSubmitEntrepreneurApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nInvestitsiya jalb qilish bo'yicha murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        Thread.Sleep(1000);
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private async Task HandleSubmitInvestmentApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nInvestorlik uchun murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        Thread.Sleep(1000);
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }
}
