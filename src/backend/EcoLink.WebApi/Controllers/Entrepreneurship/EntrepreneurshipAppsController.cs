using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;

namespace EcoLink.WebApi.Controllers.Entrepreneurship;

public class EntrepreneurshipAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEntrepreneurshipAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand(command)));


}
