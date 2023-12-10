using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgBloom.WebApi.Controllers.Commons;
using OrgBloom.Application.Queries.GetEntrepreneurs;
using OrgBloom.Application.Commands.Entrepreneurs.CreateEntrepreneurs;
using OrgBloom.Application.Commands.Entrepreneurs.DeleteEntrepreneurs;
using OrgBloom.Application.Commands.Entrepreneurs.UpdateEntrepreneurs;

namespace OrgBloom.WebApi.Controllers;

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
