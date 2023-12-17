using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

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

        await mediator.Send(new UpdateEntrepreneurProjectByUserIdCommand() { UserId = user.Id, Project = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForHelpTypeEntrepreneurshipAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAboutHelpTypeForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEntrepreneurHelpTypeByUserIdCommand() { UserId = user.Id, HelpType = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForRequiredFundingForEntrepreneurshipAsync(botClient, message, cancellationToken);
    }

    private async Task HandleRequiredFundingForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEntrepreneurRequiredFundingByUserIdCommand() { UserId = user.Id, RequiredFunding = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForAssetsInvestedForEntrepreneurshipAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAssetsInvestedForEntrepreneurshipAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEntrepreneurAssetsInvestedByUserIdCommand() { UserId = user.Id, AssetsInvested = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPhoneNumberAsync(botClient, message, cancellationToken);
    }
}
