using EcoLink.Application.Representatives.DTOs;
using EcoLink.Application.Representatives.Queries.GetRepresentatives;
using EcoLink.Application.Representatives.Commands.CreateRepresentatives;
using EcoLink.Application.Representatives.Commands.DeleteRepresentatives;
using EcoLink.Application.Representatives.Commands.UpdateRepresentatives;

namespace EcoLink.WebApi.Controllers.Representative;

public class RepresentativesController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateRepresentativeCommand command)
        => Ok(await mediator.Send(new CreateRepresentativeCommand(command)));

    [HttpPost("create-with-return")]
    [ProducesResponseType(type: typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWithReturn(CreateRepresentativeWithReturnCommand command)
        => Ok(await mediator.Send(new CreateRepresentativeWithReturnCommand(command)));

    [HttpPut("update")]
    [ProducesResponseType(typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateRepresentativeCommand command)
        => Ok(await mediator.Send(new UpdateRepresentativeCommand(command)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> Delete([FromQuery] DeleteRepresentativeCommand command)
        => Ok(await mediator.Send(new DeleteRepresentativeCommand(command)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetRepresentativeQuery command)
        => Ok(await mediator.Send(new GetRepresentativeQuery(command)));

    [HttpGet("get-by-user-id/{user-id:long}")]
    [ProducesResponseType(typeof(IEnumerable<RepresentativeResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId([FromQuery] GetRepresentativeQuery command)
        => Ok(await mediator.Send(new GetRepresentativeQuery(command)));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<RepresentativeResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllRepresentativesQuery()));
}
