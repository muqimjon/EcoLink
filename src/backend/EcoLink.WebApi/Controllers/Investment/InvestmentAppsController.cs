using EcoLink.Application.InvestmentApps.DTOs;
using EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;
using EcoLink.Application.InvestmentApps.Commands.CreateInvestment;
using EcoLink.Application.InvestmentApps.Commands.UpdateInvestment;


namespace EcoLink.WebApi.Controllers.Investment;

public class InvestmentAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateInvestmentWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new CreateInvestmentWithReturnCommand(command), cancellationToken) });

    [HttpPut("update-status")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStatus(UpdateInvestmentStatusCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new UpdateInvestmentStatusCommand(command), cancellationToken) });

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetInvestmentAppQuery(id), cancellationToken) });

    [HttpGet("get-all-by-user-user-id/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<InvestmentAppResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetAllInvestmentAppsByUserIdQuery(userId), cancellationToken) });
}
