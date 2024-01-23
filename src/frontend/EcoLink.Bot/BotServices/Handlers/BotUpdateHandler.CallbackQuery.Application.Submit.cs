using EcoLink.Domain.Entities.Representation;

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

        await serviceProvider.GetRequiredService<RepresentativeAppService>().CreateAsync(new RepresentationAppDto
        {
            UserId = user.Id,
            Area = user.Application.Area,
            Age = user.Age,
            Purpose = user.Application.Purpose,
            Degree = user.Degree,
            Address = user.Address,
            LastName = user.LastName,
            Expectation = user.Application.Expectation,
            FirstName = user.FirstName,
            Languages = user.Languages,
            Experience = user.Experience,
        }, cancellationToken);

        user.Application.IsSubmitted = true;
        await service.UpdateAsync(user, cancellationToken);
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

        await serviceProvider.GetRequiredService<ProjectManagerAppService>().CreateAsync(new ProjectManagementAppDto
        {
            UserId = user.Id,
            Age = user.Age,
            Purpose = user.Application.Purpose,
            Degree = user.Degree,
            Address = user.Address,
            LastName = user.LastName,
            Expectation = user.Application.Expectation,
            FirstName = user.FirstName,
            Languages = user.Languages,
            Experience = user.Experience,
            ProjectDirection = user.Application.ProjectDirection,
        }, cancellationToken);

        user.Application.IsSubmitted = true;
        await service.UpdateAsync(user, cancellationToken);
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

        await serviceProvider.GetRequiredService<EntrepreneurAppService>().CreateAsync(new EntrepreneurshipAppDto
        {
            UserId = user.Id,
            Age = user.Age,
            Project = user.Application.Project,
            Phone = user.Phone,
            Email = user.Email,
            HelpType = user.Application.HelpType,
            Degree = user.Degree,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Experience = user.Experience,
            AssetsInvested = user.Application.AssetsInvested,
            RequiredFunding = user.Application.RequiredFunding,
        }, cancellationToken);

        user.Application.IsSubmitted = true;
        await service.UpdateAsync(user, cancellationToken);
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
        
        await serviceProvider.GetRequiredService<InvestmentAppService>().CreateAsync(new InvestmentAppDto
        {
            UserId = user.Id,
            Age = user.Age,
            Sector = user.Application.Sector,
            Phone = user.Phone,
            Email = user.Email,
            Degree = user.Degree,
            LastName = user.LastName,
            FirstName = user.FirstName,
            InvestmentAmount = user.Application.InvestmentAmount
        }, cancellationToken);

        user.Application.IsSubmitted = true;
        await service.UpdateAsync(user, cancellationToken);
    }
}
