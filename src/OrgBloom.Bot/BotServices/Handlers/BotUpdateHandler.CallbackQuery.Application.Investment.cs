using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSectorAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var sector = callbackQuery.Data switch
        {
            "sectorIT" => "Axborot texnologiyalari",
            "sectorManufacturing" => "Ishlab chiqarish",
            "sectorTrade" => "Savdo",
            "sectorConstruction" => "Qurilish",
            "sectorAgriculture" => "Qishloq xo'jaligi",
            "sectorEnergy" => "Energetika",
            "sectorEducation" => "Ta'lim",
            "sectorFranchise" => "Franshiza",
            _ => string.Empty,
        };

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        switch(profession)
        {
            case UserProfession.Investor:
                await mediator.Send(new UpdateInvestorSectorByUserIdCommand { UserId = user.Id, Sector = sector }, cancellationToken);
                await SendRequestForInvestmentAmountForInvestmentAsync(botClient, callbackQuery.Message, cancellationToken);
                break;
            case UserProfession.ProjectManager:
                await mediator.Send(new UpdateProjectManagerProjectDirectionByUserIdCommand() { UserId = user.Id, ProjectDirection = sector }, cancellationToken);
                await SendRequestForExpectationAsync(botClient, callbackQuery.Message, cancellationToken);
                break;
            default:
                await HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken);
                break;
        };
    }
}
