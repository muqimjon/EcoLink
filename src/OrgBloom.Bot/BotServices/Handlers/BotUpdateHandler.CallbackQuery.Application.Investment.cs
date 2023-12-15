using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSectorAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var handle = callbackQuery.Data switch
        {
            "sectorIT" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "IT" }, cancellationToken),
            "sectorManufacturing" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "Ishlab chiqarish" }, cancellationToken),
            "sectorTrade" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "Savdo" }, cancellationToken),
            "sectorConstruction" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "Qurilish" }, cancellationToken),
            "sectorAgriculture" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "Qishloq xo'jaligi" }, cancellationToken),
            "sectorEnergy" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "Energetika" }, cancellationToken),
            "sectorEducation" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "Ta'lim" }, cancellationToken),
            "sectorFranchise" => mediator.Send(new UpdateInvestorSectorCommand { Id = user.Id, Sector = "Franshiza" }, cancellationToken),
            _ => HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }

        await SendRequestForInvestmentAmountForInvestmentAsync(botClient, callbackQuery.Message, cancellationToken);
    }
}
