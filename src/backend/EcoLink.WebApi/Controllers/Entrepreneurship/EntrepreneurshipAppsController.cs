using EcoLink.Application.Apps.Commands.CreateEntrepreneurs;
using EcoLink.Application.EntrepreneurshipApps.Commands.UpdateEntrepreneurship;
using EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

namespace EcoLink.WebApi.Controllers.Entrepreneurship;

public class EntrepreneurshipAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEntrepreneurshipWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new CreateEntrepreneurshipWithReturnCommand(command), cancellationToken) });

    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateStatus(UpdateEntrepreneurshipStatusCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new UpdateEntrepreneurshipStatusCommand(command), cancellationToken) });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetEntrepreneurshipAppByIdCommand(id), cancellationToken) });

    [HttpGet("get-all-by-user-id/{userId:long}")]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetAllEntrepreneurshipAppsByUserIdQuery(userId), cancellationToken) });
}
