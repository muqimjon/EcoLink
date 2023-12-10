using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgBloom.Application.Queries.GetInvestors;
using OrgBloom.Application.Commands.Investors.CreateInvestors;
using OrgBloom.Application.Commands.Investors.DeleteInvestors;
using OrgBloom.Application.Commands.Investors.UpdateInvestors;

namespace OrgBloom.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestorsController(IMediator mediator) : ControllerBase
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