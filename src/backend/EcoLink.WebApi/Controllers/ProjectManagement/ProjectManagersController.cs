using EcoLink.Application.ProjectManagers.DTOs;
using EcoLink.Application.ProjectManagers.Queries.GetProjectManagers;
using EcoLink.Application.ProjectManagers.Commands.CreateProjectManagers;
using EcoLink.Application.ProjectManagers.Commands.DeleteProjectManagers;
using EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;

namespace EcoLink.WebApi.Controllers.ProjectManagement;

public class ProjectManagersController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateProjectManagerCommand command)
        => Ok(await mediator.Send(new CreateProjectManagerCommand(command)));

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWithReturn(CreateProjectManagerWithReturnCommand command)
        => Ok(await mediator.Send(new CreateProjectManagerWithReturnCommand(command)));

    [HttpPut("update")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateProjectManagerCommand command)
        => Ok(await mediator.Send(new UpdateProjectManagerCommand(command)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> Delete([FromQuery] DeleteProjectManagerCommand command)
        => Ok(await mediator.Send(new DeleteProjectManagerCommand(command)));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> Get([FromQuery] GetProjectManagerQuery command)
        => Ok(await mediator.Send(new GetProjectManagerQuery(command)));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllProjectManagersQuery()));
}
