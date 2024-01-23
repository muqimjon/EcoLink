using EcoLink.Application.InvestmentApps.DTOs;
using EcoLink.Application.InvestmentApps.Commands.CreateInvestmentApps;
using EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;


namespace EcoLink.WebApi.Controllers.Investment;

public class InvestmentAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateInvestmentAppWithReturnCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new CreateInvestmentAppWithReturnCommand(command), cancellationToken) });

    [HttpGet("get-all-by-user-userId/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<InvestmentAppResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetAllInvestmentAppsByUserIdQuery(userId), cancellationToken) });

    [HttpGet("get/{userId:long}")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetInvestmentAppQuery(id), cancellationToken) });
}
