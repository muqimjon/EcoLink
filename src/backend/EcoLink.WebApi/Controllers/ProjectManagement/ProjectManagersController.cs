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
    public async Task<IActionResult> Create(CreateProjectManagerCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new CreateProjectManagerCommand(command), cancellationToken) });

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWithReturn(CreateProjectManagerWithReturnCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new CreateProjectManagerWithReturnCommand(command), cancellationToken) });

    [HttpPut("update")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateProjectManagerCommand command, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new UpdateProjectManagerCommand(command), cancellationToken) });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new DeleteProjectManagerCommand(id), cancellationToken) });

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetProjectManagerQuery(id), cancellationToken) });

    [HttpGet("get-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(long userId, CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetProjectManagerByUserIdQuery(userId), cancellationToken) });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<ProjectManagerResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        => Ok(new Response { Data = await mediator.Send(new GetAllProjectManagersQuery(), cancellationToken) });
}
