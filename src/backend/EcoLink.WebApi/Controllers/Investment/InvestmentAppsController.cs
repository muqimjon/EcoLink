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

    [HttpGet("get-all-by-user-userId/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<InvestmentAppResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId)
        => Ok(await mediator.Send(new GetAllInvestmentAppsByUserIdQuery(userId)));

    [HttpGet("get/{userId:long}")]
    [ProducesResponseType(typeof(InvestmentAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id)
        => Ok(await mediator.Send(new GetInvestmentAppQuery(id)));
}
