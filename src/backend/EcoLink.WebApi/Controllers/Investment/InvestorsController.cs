using EcoLink.Application.Investors.DTOs;
using EcoLink.Application.Investors.Queries.GetInvestors;
using EcoLink.Application.Investors.Commands.CreateInvestors;
using EcoLink.Application.Investors.Commands.DeleteInvestors;
using EcoLink.Application.Investors.Commands.UpdateInvestors;

namespace EcoLink.WebApi.Controllers.Investment;

public class InvestorsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateInvestorCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new CreateInvestorCommand(command), cancellationToken) });

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(InvestorResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWithReturn(CreateInvestorWithReturnCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new CreateInvestorWithReturnCommand(command), cancellationToken) });

    [HttpPut("update")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateInvestorCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new UpdateInvestorCommand(command), cancellationToken) });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new DeleteInvestorCommand(id), cancellationToken) });

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(InvestorResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetInvestorQuery(id), cancellationToken) });

    [HttpGet("get-by-user-id/{userId:long:}")]
    [ProducesResponseType(typeof(InvestorResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(long userId, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetInvestorByUserIdQuery(userId), cancellationToken) });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<InvestorResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetAllInvestorsQuery(), cancellationToken) });
}