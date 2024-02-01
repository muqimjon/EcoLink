using EcoLink.Domain.Entities.Representation;
using EcoLink.Application.RepresentationApps.Queries.GetRepresentationApp;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentation;
using EcoLink.Application.RepresentationApps.Commands.UpdateRepresentation;

namespace EcoLink.WebApi.Controllers.Representative;

public class RepresentationAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateRepresentationWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new CreateRepresentationWithReturnCommand(command), cancellationToken) });

    [HttpPut("update-status")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStatus(UpdateRepresentationStatusCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new UpdateRepresentationStatusCommand(command), cancellationToken) });

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetRepresentationAppByIdQuery(id), cancellationToken) });

    [HttpGet("get-all-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<RepresentationApp>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetAllRepresentationAppsByUserIdQuery(userId), cancellationToken) });
}
