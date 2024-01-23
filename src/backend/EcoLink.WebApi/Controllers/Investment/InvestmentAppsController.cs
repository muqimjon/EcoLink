using EcoLink.Application.InvestmentApps.DTOs;
using EcoLink.Application.InvestmentApps.Commands.CreateInvestmentApps;
using EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;


namespace EcoLink.WebApi.Controllers.Investment;

public class InvestmentAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateInvestmentAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateInvestmentAppWithReturnCommand(command)));

    [HttpGet("get-all/{user-id:long}")]
    [ProducesResponseType(typeof(IEnumerable<InvestmentAppResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetAllInvestmentAppsByUserIdQuery query)
        => Ok(await mediator.Send(new GetAllInvestmentAppsByUserIdQuery(query.UserId)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetInvestmentAppQuery query)
        => Ok(await mediator.Send(new GetInvestmentAppQuery(query.Id)));
}
