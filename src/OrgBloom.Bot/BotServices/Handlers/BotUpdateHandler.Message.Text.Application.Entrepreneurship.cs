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
            await SendAlreadyExistApplicationAsync(StringHelper.GetEntrepreneurshipApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAboutProjectForEntrepreneurship(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEntrepreneurProjectCommand() { Id = user.Id, Project = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForHelpTypeEntrepreneurshipAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAboutHelpTypeForEntrepreneurship(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEntrepreneurHelpTypeCommand() { Id = user.Id, HelpType = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForRequiredFundingForEntrepreneurshipAsync(botClient, message, cancellationToken);
    }

    private async Task HandleRequiredFundingForEntrepreneurship(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEntrepreneurRequiredFundingCommand() { Id = user.Id, RequiredFunding = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForAssetsInvestedForEntrepreneurshipAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAssetsInvestedForEntrepreneurship(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEntrepreneurAssetsInvestedCommand() { Id = user.Id, AssetsInvested = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPhoneNumberAsync(botClient, message, cancellationToken);
    }
}
