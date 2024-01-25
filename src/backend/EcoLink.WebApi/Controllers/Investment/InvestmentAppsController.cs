using EcoLink.Application.InvestmentApps.DTOs;
using EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;
using EcoLink.Application.InvestmentApps.Commands.CreateInvestmentApps;


namespace EcoLink.WebApi.Controllers.Investment;

public class InvestmentAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateInvestmentAppWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateInvestmentAppWithReturnCommand(command), cancellationToken));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetInvestmentAppQuery(id), cancellationToken));

    [HttpGet("get-all-by-user-userId/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<InvestmentAppResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetAllInvestmentAppsByUserIdQuery(userId), cancellationToken));
}
