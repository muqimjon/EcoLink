using OrgBloom.Application.Representatives.Queries.GetRepresentatives;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.Representatives.Commands.DeleteRepresentatives;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

namespace OrgBloom.WebApi.Controllers;

public class RepresentativesController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateRepresentativeCommand command)
        => Ok(await mediator.Send(new CreateRepresentativeCommand(command)));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateRepresentativeCommand command)
        => Ok(await mediator.Send(new UpdateRepresentativeCommand(command)));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteRepresentativeCommand command)
        => Ok(await mediator.Send(new DeleteRepresentativeCommand(command)));

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] GetRepresentativeQuery command)
        => Ok(await mediator.Send(new GetRepresentativeQuery(command)));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllRepresentativesQuery()));
}
