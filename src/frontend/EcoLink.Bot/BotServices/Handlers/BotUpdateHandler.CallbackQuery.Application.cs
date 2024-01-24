namespace EcoLink.Bot.BotServices;

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

        var sector = callbackQuery.Data switch
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
            text: localizer["txtSelected", sector],
            cancellationToken: cancellationToken);

        switch (user.Profession)
        {
            case UserProfession.Investor:
                user.Investment.Sector = sector;
                await SendRequestForInvestmentAmountForInvestmentAsync(botClient, callbackQuery.Message, cancellationToken);
                break;
            case UserProfession.ProjectManager:
                user.ProjectManagement.ProjectDirection = sector;
                await SendRequestForExpectationAsync(botClient, callbackQuery.Message, cancellationToken);
                break;
            case UserProfession.Entrepreneur:
                user.Entrepreneurship.Sector = sector;
                await SendRequestForAboutProjectForEntrepreneurshipAsync(botClient, callbackQuery.Message, cancellationToken);
                break;
            default:
                await HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken);
                break;
        };
    }

    private Task HandleUnknownSubmissionAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown callback query: {callbackQuery.Data}", callbackQuery.Data);
        return Task.CompletedTask;
    }
}
