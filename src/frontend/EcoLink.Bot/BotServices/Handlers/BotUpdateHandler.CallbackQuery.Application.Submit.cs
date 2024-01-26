using EcoLink.ApiService.Models.Investment;
using EcoLink.ApiService.Models.Representation;
using EcoLink.ApiService.Models.Entrepreneurship;
using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSubmitApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var handle = user.Profession switch
        {
            UserProfession.Entrepreneur => HandleSubmitEntrepreneurApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Investor => HandleSubmitInvestmentApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.ProjectManager => HandleSubmitProjectManagerApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Representative => HandleSubmitRepresentativeApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownSubmissionAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query application submit: {callbackQuery.Data}", callbackQuery.Data); }
        
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private async Task HandleSubmitRepresentativeApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        await representationAppService.AddAsync(new RepresentationAppDto
        {
            UserId = user.Id,
            Area = user.Area,
            Age = user.Age,
            Purpose = user.Purpose,
            Degree = user.Degree,
            Address = user.Address,
            LastName = user.LastName,
            Expectation = user.Expectation,
            FirstName = user.FirstName,
            Languages = user.Languages,
            Experience = user.Experience,
        }, cancellationToken);
    }

    private async Task HandleSubmitProjectManagerApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        await projectManagementAppService.AddAsync(new ProjectManagementAppDto
        {
            UserId = user.Id,
            Age = user.Age,
            Purpose = user.Purpose,
            Degree = user.Degree,
            Address = user.Address,
            LastName = user.LastName,
            Expectation = user.Expectation,
            FirstName = user.FirstName,
            Languages = user.Languages,
            Experience = user.Experience,
            Sector = user.Sector,
        }, cancellationToken);
    }

    private async Task HandleSubmitEntrepreneurApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        await entrepreneurshipAppService.AddAsync(new EntrepreneurshipAppDto
        {
            UserId = user.Id,
            Age = user.Age,
            Project = user.Project,
            Phone = user.Phone,
            Email = user.Email,
            HelpType = user.HelpType,
            Degree = user.Degree,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Experience = user.Experience,
            AssetsInvested = user.AssetsInvested,
            RequiredFunding = user.RequiredFunding,
        }, cancellationToken);
    }

    private async Task HandleSubmitInvestmentApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        await investmentAppService.AddAsync(new InvestmentAppDto
        {
            UserId = user.Id,
            Age = user.Age,
            Sector = user.Sector,
            Phone = user.Phone,
            Email = user.Email,
            Degree = user.Degree,
            LastName = user.LastName,
            FirstName = user.FirstName,
            InvestmentAmount = user.InvestmentAmount
        }, cancellationToken);
    }
}
