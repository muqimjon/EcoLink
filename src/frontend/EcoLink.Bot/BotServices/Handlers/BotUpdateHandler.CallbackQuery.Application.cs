namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSubmittionApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var handler = callbackQuery.Data switch
        {
            "submit" => HandleSubmitApplicationAsync(botClient, callbackQuery, cancellationToken),
            "cancel" => HandleCancelApplication(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownSubmissionAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }
    }

    private async Task HandleCancelApplication(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);


        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtCancelApplication"]}", 
            cancellationToken: cancellationToken);

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }
    
    private async Task HandleSectorAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        user.Sector = callbackQuery.Data switch
        {
            "sectorIT" => "Axborot texnologiyalari",
            "sectorManufacturing" => "Ishlab chiqarish",
            "sectorTrade" => "Savdo",
            "sectorConstruction" => "Qurilish",
            "sectorAgriculture" => "Qishloq xo'jaligi",
            "sectorEnergy" => "Energetika",
            "sectorEducation" => "Ta'lim",
            "sectorFranchise" => "Franshiza",
            _ => string.Empty,
        };

         await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: localizer["txtSelected", user.Sector],
            cancellationToken: cancellationToken);

        var handler = user.Profession switch
        {
            UserProfession.Investor => SendRequestForInvestmentAmountForInvestmentAsync(botClient, callbackQuery.Message, cancellationToken),
            UserProfession.ProjectManager => SendRequestForExpectationAsync(botClient, callbackQuery.Message, cancellationToken),
            UserProfession.Entrepreneur => SendRequestForAboutProjectForEntrepreneurshipAsync(botClient, callbackQuery.Message, cancellationToken),
            _ => HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken),
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }
    }

    private Task HandleUnknownSubmissionAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown callback query: {callbackQuery.Data}", callbackQuery.Data);
        return Task.CompletedTask;
    }
}
