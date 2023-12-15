using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task SendRequestForSectorAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("IT", "sectorIT") },
            new[] { InlineKeyboardButton.WithCallbackData("Ishlab chiqarish", "sectorManufacturing") },
            new[] { InlineKeyboardButton.WithCallbackData("Savdo", "sectorTrade") },
            new[] { InlineKeyboardButton.WithCallbackData("Qurilish", "sectorConstruction") },
            new[] { InlineKeyboardButton.WithCallbackData("Qishloq xo'jaligi", "sectorAgriculture") },
            new[] { InlineKeyboardButton.WithCallbackData("Energetika", "sectorEnergy") },
            new[] { InlineKeyboardButton.WithCallbackData("Ta'lim", "sectorEducation") },
            new[] { InlineKeyboardButton.WithCallbackData("Franshiza", "sectorFranchise") }
        });

        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: "Qaysi sohada investorlik qilmoqchisiz?",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard);

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterSector), cancellationToken);
    }

    private async Task SendRequestForInvestmentAmountAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: "Qancha miqdorda investitsiya kiritmoqchisiz?\nDollardayozing:",
            cancellationToken: cancellationToken,
            replyMarkup: new ReplyKeyboardRemove());

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterInvestmentAmount), cancellationToken);
    }

    private async Task SendForSubmitInvestmentApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var investor = await mediator.Send(new GetInvestorByUserIdQuery() { UserId = user.Id }, cancellationToken);

        var keyboard = new InlineKeyboardMarkup(new[] {
            new[] { InlineKeyboardButton.WithCallbackData("Tasdiqlash", "submit") },
            new[] { InlineKeyboardButton.WithCallbackData("E'tiborsiz qoldrish", "cancel") }
        });

        var text = $"Ma'lumotlarni tasdiqlang:\n" +
            $"Ism: {investor.User.FirstName}\n" +
            $"Familiya: {investor.User.LastName}\n" +
            $"Otasining ismi: {investor.User.Patronomyc}\n" +
            $"Yoshi: {(DateTime.UtcNow - investor.User.DateOfBirth).ToString()!.Split().First()}";
        await botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: text,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSubmitApplication), cancellationToken);
    }
}
