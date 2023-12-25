using EcoLink.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.DeleteEntrepreneurs;

namespace EcoLink.WebApi.Controllers;

public class EntrepreneursController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEntrepreneurCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurCommand(command)));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateEntrepreneurCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurCommand(command)));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteEntrepreneurCommand command)
        => Ok(await mediator.Send(new DeleteEntrepreneurCommand(command)));

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] GetEntrepreneurQuery command)
        => Ok(await mediator.Send(new GetEntrepreneurQuery(command)));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllEntrepreneursQuery()));
}
