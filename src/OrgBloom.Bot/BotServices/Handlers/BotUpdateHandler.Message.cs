using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task HandleMessageAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        logger.LogInformation("Received message from {from.FirstName}", user.FirstName);

        var handler = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsync(botClient, message, cancellationToken),
            MessageType.Contact => HandleContactMessageAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken),
        };

        await handler;
    }

    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            "/start" => SendGreeting(botClient, message, cancellationToken),
            "Ariza topshirish" => SendApplyQuery(botClient, message, cancellationToken),
            "Investorlik qilish" => InvestorQuery(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try
        {
            await handler;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName);
        }
    }

    private async Task InvestorQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var inverstor = await mediator.Send(new GetInvestorByUserIdQuery() { UserId = user.Id }, cancellationToken);
        if(inverstor is null)
            await SendAlreadyExistApplicationAsync(inverstor!, botClient, message, cancellationToken);
        else
        {
            await mediator.Send(new UpdateProfessionCommand() { Id = user.Id , Profession = UserProfession.Investor }, cancellationToken);
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
            await mediator.Send(new CreateInvestorWithReturnCommand() { UserId = user.Id }, cancellationToken);
        }
    }

    private Task SendAlreadyExistApplicationAsync(InvestorResultDto dto, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private Task HandleUnknownMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received message type {message.Type} from {from.FirstName}", message.Type, message.From?.FirstName);

        return Task.CompletedTask;
    }
}
