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
    public async Task<IActionResult> Delete(long id)
        => Ok(await mediator.Send(new DeleteRepresentativeCommand(id)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id)
        => Ok(await mediator.Send(new GetRepresentativeQuery(id)));

    [HttpGet("get-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(long userId)
        => Ok(await mediator.Send(new GetRepresentativeByUserIdQuery(userId)));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<RepresentativeResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllRepresentativesQuery()));
}
