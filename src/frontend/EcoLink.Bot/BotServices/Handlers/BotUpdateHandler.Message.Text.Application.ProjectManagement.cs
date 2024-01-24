using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSelectedProjectManagementMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnApply"] => ProjectManagementQueryAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInfo"] => SendProfessionInfoAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
        user.Profession = UserProfession.ProjectManager;
    }

    private async Task ProjectManagementQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        user.ProjectManagement ??= await projectManagementService.AddAsync(new ProjectManagementDto() { UserId = user.Id }, cancellationToken);

        if (user.ProjectManagement.IsSubmitted)
            await SendAlreadyExistApplicationAsync(botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleProjectDirectionForProjectManagementAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
            {
                UserProfession.ProjectManager => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.ProjectManager => SendRequestForExpectationAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
            user.ProjectManagement.ProjectDirection = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}
