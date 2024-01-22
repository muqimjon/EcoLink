using EcoLink.Application.Investors.Commands.CreateInvestors;
using EcoLink.Application.Investors.Commands.DeleteInvestors;
using EcoLink.Application.Investors.Commands.UpdateInvestors;
using EcoLink.Application.Investors.Queries.GetInvestors;

namespace EcoLink.WebApi.Controllers.Investment;

public class InvestorsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateInvestorCommand command)
        => Ok(await mediator.Send(new CreateInvestorCommand(command)));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateInvestorCommand command)
        => Ok(await mediator.Send(new UpdateInvestorCommand(command)));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteInvestorCommand command)
        => Ok(await mediator.Send(new DeleteInvestorCommand(command)));

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] GetInvestorQuery command)
        => Ok(await mediator.Send(new GetInvestorQuery(command)));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllInvestorsQuery()));
}