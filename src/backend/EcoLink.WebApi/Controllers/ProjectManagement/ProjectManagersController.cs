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
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id)
        => Ok(await mediator.Send(new DeleteProjectManagerCommand(id)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id)
        => Ok(await mediator.Send(new GetProjectManagerQuery(id)));

    [HttpGet("get-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(long userId)
        => Ok(await mediator.Send(new GetProjectManagerByUserIdQuery(userId)));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<ProjectManagerResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllProjectManagersQuery()));
}
