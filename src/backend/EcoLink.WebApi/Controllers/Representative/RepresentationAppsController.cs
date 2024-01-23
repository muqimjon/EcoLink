using EcoLink.Domain.Entities.Representation;
using EcoLink.Application.Representatives.Queries.GetRepresentatives;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentationApps;

namespace EcoLink.WebApi.Controllers.Representative;

public class RepresentationAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateRepresentationAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateRepresentationAppWithReturnCommand(command)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetRepresentativeQuery query)
        => Ok(await mediator.Send(new GetRepresentativeQuery(query)));

    [HttpGet("get-all-by-user-id")]
    [ProducesResponseType(typeof(IEnumerable<RepresentationApp>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId([FromQuery] GetRepresentativeQuery query)
        => Ok(await mediator.Send(new GetRepresentativeQuery(query)));
}
