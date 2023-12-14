using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var userState = State.WaitingForSubmitApplication;

        if (message.Text is not "/start")
            userState = await mediator.Send(new GetStateQuery(user.Id), cancellationToken);

        var handler = userState switch
        {
            State.WaitingForSubmitApplication => SendGreetingAsync(botClient, message, cancellationToken),
            State.WaitingForSelectMainMenu => HandleMainMenuAsync(botClient, message, cancellationToken),
            State.WaitingForSelectProfession => HandleProfessionAsync(botClient, message, cancellationToken),
            State.WaitingForEnterFirstName => HandleFirstNameAsync(botClient, message, cancellationToken),
            State.WaitingForEnterLastName => HandleLastNameAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPatronomyc => HandlePatronomycAsync(botClient, message, cancellationToken),
            State.WaitingForEnterDateOfBirth => HandleDateOfBirthAsync(botClient, message, cancellationToken),
            State.WaitingForEnterDegree => HandleDegreeAsync(botClient, message, cancellationToken),
            State.WaitingForEnterSector => HandleSectorAsync(botClient, message, cancellationToken),
            State.WaitingForEnterInvestmentAmount => HandleInvestmentAmountAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task HandleMainMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            "Ariza topshirish" => SendApplyQuery(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };


        try { await handle; }
        catch(Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task HandleProfessionAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            "Investorlik qilish" => InvestorQuery(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };


        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task InvestorQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var application = await mediator.Send(new GetInvestorByUserIdQuery() { UserId = user.Id }, cancellationToken)
            ?? await mediator.Send(new CreateInvestorWithReturnCommand() { UserId = user.Id }, cancellationToken);
        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(application, botClient, message, cancellationToken);
        else
        {
            await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Investor }, cancellationToken);
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
        }
    }

    private async Task HandleFirstNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateFirstNameCommand() { Id = user.Id, FirstName = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForLastNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleLastNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateLastNameCommand() { Id = user.Id, LastName = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPatronomycAsync(botClient, message, cancellationToken);
    }

    private async Task HandlePatronomycAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdatePatronomycCommand() { Id = user.Id, Patronomyc = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForDateOfBirthAsync(botClient, message, cancellationToken);
    }

    private async Task HandleDateOfBirthAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        if (DateTime.TryParse(message.Text, out DateTime dateOfBirth))
        {
            await mediator.Send(new UpdateDateOfBirthCommand() { Id = user.Id, DateOfBirth = dateOfBirth.ToUniversalTime() }, cancellationToken);
            await SendRequestForDegreeAsync(botClient, message, cancellationToken);
        }
        else await SendRequestForDateOfBirthAsync(botClient, message, cancellationToken);
    }

    private async Task HandleDegreeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateDegreeCommand() { Id = user.Id, Degree = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForSectorAsync(botClient, message, cancellationToken);
    }

    private async Task HandleSectorAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateInvestorSectorCommand() { Id = user.Id, Sector = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForInvestmentAmountAsync(botClient, message, cancellationToken);
    }

    private async Task HandleInvestmentAmountAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateInvestorInvestmentAmountCommand() { Id = user.Id, InvestmentAmount = message.Text }, cancellationToken); // TODO: need validation

        await SendForSubmitInvestmentApplicationAsync(botClient, message, cancellationToken);
    }
}
