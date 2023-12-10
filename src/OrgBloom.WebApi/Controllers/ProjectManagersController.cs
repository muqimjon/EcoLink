using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgBloom.WebApi.Controllers.Commons;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.DeleteProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;

namespace OrgBloom.WebApi.Controllers;

public class ProjectManagersController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateProjectManagerCommand command)
        => Ok(await mediator.Send(new CreateProjectManagerCommand(command)));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateProjectManagerCommand command)
        => Ok(await mediator.Send(new UpdateProjectManagerCommand(command)));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteProjectManagerCommand command)
        => Ok(await mediator.Send(new DeleteProjectManagerCommand(command)));

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] GetProjectManagerQuery command)
        => Ok(await mediator.Send(new GetProjectManagerQuery(command)));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllProjectManagersQuery()));
}
