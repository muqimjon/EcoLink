using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.Users.Queries.GetUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task EntrepreneurshipQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Entrepreneur }, cancellationToken);
        var application = await mediator.Send(new GetEntrepreneurByUserIdQuery(user.Id), cancellationToken)
            ?? await mediator.Send(new CreateEntrepreneurWithReturnCommand() { UserId = user.Id }, cancellationToken);

        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAboutProjectForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;

        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateEntrepreneurProjectByUserIdCommand() { UserId = user.Id, Project = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForHelpTypeEntrepreneurshipAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleAboutHelpTypeForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;

        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateEntrepreneurHelpTypeByUserIdCommand() { UserId = user.Id, HelpType = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForRequiredFundingForEntrepreneurshipAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleRequiredFundingForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;

        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateEntrepreneurRequiredFundingByUserIdCommand() { UserId = user.Id, RequiredFunding = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForAssetsInvestedForEntrepreneurshipAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandleAssetsInvestedForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;

        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendApplyQueryAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateEntrepreneurAssetsInvestedByUserIdCommand() { UserId = user.Id, AssetsInvested = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendSettingsQueryAsync(botClient, message, cancellationToken),
                _ => SendRequestForPhoneNumberAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}
