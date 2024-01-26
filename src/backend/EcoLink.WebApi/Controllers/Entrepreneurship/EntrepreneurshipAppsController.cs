using EcoLink.Application.Apps.Commands.CreateEntrepreneurs;
using EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

namespace EcoLink.WebApi.Controllers.Entrepreneurship;

public class EntrepreneurshipAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEntrepreneurshipAppWithReturnCommand command, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand(command), cancellationToken));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetEntrepreneurshipAppByIdCommand(id), cancellationToken));

    [HttpGet("get-all-by-user-id/{userId:long}")]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetAllEntrepreneurshipAppsByUserIdQuery(userId), cancellationToken));
}
