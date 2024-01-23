using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

namespace EcoLink.WebApi.Controllers.Entrepreneurship;

public class EntrepreneurshipAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEntrepreneurshipAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand(command)));

    [HttpGet("get-all-by-user-id/{userId:long}")]
    public async Task<IActionResult> GetAllByUserId(long userId)
        => Ok(await mediator.Send(new GetAllEntrepreneurshipAppsByUserIdQuery(userId)));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> Get(long id)
        => Ok(await mediator.Send(new GetEntrepreneurshipAppByIdCommand(id)));

}
