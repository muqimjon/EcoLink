using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Commands.UpdateUsers;

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
}
