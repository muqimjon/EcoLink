using EcoLink.Domain.Entities.Representation;
using EcoLink.Application.RepresentationApps.Queries.GetRepresentationApp;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentationApps;

namespace EcoLink.WebApi.Controllers.Representative;

public class RepresentationAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateRepresentationAppWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateRepresentationAppWithReturnCommand(command), cancellationToken));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetRepresentationAppByIdQuery(id), cancellationToken));

    [HttpGet("get-all-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<RepresentationApp>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetRepresentationAppByIdQuery(userId), cancellationToken));
}
