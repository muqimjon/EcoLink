using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

namespace EcoLink.WebApi.Controllers.Entrepreneurship;

public class EntrepreneurshipAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEntrepreneurshipAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand(command)));

    [HttpGet("get-all-by-user-id/{user-id:long}")]
    public async Task<IActionResult> GetAllByUserId([FromQuery] GetAllEntrepreneurshipAppsByUserIdQuery query)
        => Ok(await mediator.Send(new GetAllEntrepreneurshipAppsByUserIdQuery(query.UserId)));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> Get([FromQuery]GetEntrepreneurshipAppByIdCommand query)
        => Ok(await mediator.Send(new GetEntrepreneurshipAppByIdCommand(query.Id)));

}
