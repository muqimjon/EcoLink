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
    public async Task<IActionResult> Create(CreateRepresentativeCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new CreateRepresentativeCommand(command), cancellationToken) });

    [HttpPost("create-with-return")]
    [ProducesResponseType(type: typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWithReturn(CreateRepresentativeWithReturnCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new CreateRepresentativeWithReturnCommand(command), cancellationToken) });

    [HttpPut("update")]
    [ProducesResponseType(typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateRepresentativeCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new UpdateRepresentativeCommand(command), cancellationToken) });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new DeleteRepresentativeCommand(id), cancellationToken) });

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetRepresentativeQuery(id), cancellationToken) });

    [HttpGet("get-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(RepresentativeResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(long userId, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetRepresentativeByUserIdQuery(userId), cancellationToken) });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<RepresentativeResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetAllRepresentativesQuery(), cancellationToken) });
}
