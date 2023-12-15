using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

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
            State.WaitingForEnterInvestmentAmount => HandleInvestmentAmountAsync(botClient, message, cancellationToken),
            State.WaitingForEnterEmail => HandleEmailAsync(botClient, message, cancellationToken),
            State.WaitingForResendApplication => HandleResendApplicationAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPhoneNumber => HandlePhoneNumbeFromTextAsync(botClient, message, cancellationToken),
            State.WaitingForEnterLanguages => HandleLanguagesAsync(botClient, message, cancellationToken),
            State.WaitingForEnterExperience => HandleExperienceAsync(botClient, message, cancellationToken),
            State.WaitingForEnterAddress => HandleAddressAsync(botClient, message, cancellationToken),
            State.WaitingForEnterAreaToRepresentation => HandleAreaAsync(botClient, message, cancellationToken),
            State.WaitingForEnterExpectationForRepresentation => HandleExpectationForRepresentationAsync(botClient, message, cancellationToken),
            State.WaitingForEnterPurposeForRepresentation => HandlePurposeForRepresentationAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        }; ;

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
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

        await SendRequestForLanguagesAsync(botClient, message, cancellationToken);
    }

    private async Task HandleInvestmentAmountAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateInvestorInvestmentAmountCommand() { Id = user.Id, InvestmentAmount = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPhoneNumberAsync(botClient, message, cancellationToken);
    }

    private async Task HandlePhoneNumbeFromTextAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdatePhoneCommand() { Id = user.Id, Phone = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForEmailAsync(botClient, message, cancellationToken);
    }

    private async Task HandleEmailAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateEmailCommand() { Id = user.Id, Email = message.Text }, cancellationToken); // TODO: need validation

        await SendForSubmitInvestmentApplicationAsync(botClient, message, cancellationToken);
    }

    private async Task HandleLanguagesAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateLanguagesCommand() { Id = user.Id, Languages = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForExperienceAsync(botClient, message, cancellationToken);
    }

    private async Task HandleExperienceAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateExperienceCommand() { Id = user.Id, Experience = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForAddressAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAddressAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateAddressCommand() { Id = user.Id, Address = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForAreaAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateRepresentativeAreaCommand() { Id = user.Id, Area = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForExpectationAsync(botClient, message, cancellationToken);
    }

    private async Task HandleExpectationForRepresentationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateRepresentativeExpectationCommand() { Id = user.Id, Expectation = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPurposeAsync(botClient, message, cancellationToken);
    }

    private async Task HandlePurposeForRepresentationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateRepresentativePurposeCommand() { Id = user.Id, Purpose = message.Text }, cancellationToken); // TODO: need validation

        await SendForSubmitInvestmentApplicationAsync(botClient, message, cancellationToken);
    }
}
