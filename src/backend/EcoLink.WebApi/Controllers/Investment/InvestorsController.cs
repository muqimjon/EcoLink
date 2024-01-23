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
    public async Task<IActionResult> Create(CreateInvestorCommand command)
        => Ok(await mediator.Send(new CreateInvestorCommand(command)));

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(InvestorResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWithReturn(CreateInvestorWithReturnCommand command)
        => Ok(await mediator.Send(new CreateInvestorWithReturnCommand(command)));

    [HttpPut("update")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateInvestorCommand command)
        => Ok(await mediator.Send(new UpdateInvestorCommand(command)));

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(DeleteInvestorCommand command)
        => Ok(await mediator.Send(new DeleteInvestorCommand(command)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(InvestorResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetInvestorQuery command)
        => Ok(await mediator.Send(new GetInvestorQuery(command)));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<InvestorResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllInvestorsQuery()));
}