using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using OrgBloom.Application.Users.Queries.GetUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        //var handler = message.Text switch
        //{
        //    "/start" => SendGreeting(botClient, message, cancellationToken),
        //    "Ariza topshirish" => SendApplyQuery(botClient, message, cancellationToken),
        //    "Investorlik qilish" => InvestorQuery(botClient, message, cancellationToken),
        //    _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        //};

        if (message.Text == "/start")
        {
            await SendGreeting(botClient, message, cancellationToken); return;
        }

        var userState = await mediator.Send(new GetStateQuery(user.Id), cancellationToken);
        var handler = userState switch
        {
            State.None => SendGreeting(botClient, message, cancellationToken),
            State.WaitingForSelectMainMenu => HandleMainMenuAsync(botClient, message, cancellationToken),
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
            "Investorlik qilish" => InvestorQuery(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };


        try { await handle; }
        catch(Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
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

}
