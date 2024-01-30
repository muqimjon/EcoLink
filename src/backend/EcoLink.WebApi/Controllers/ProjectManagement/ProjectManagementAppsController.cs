using EcoLink.Application.ProjectManagementApps.DTOs;
using EcoLink.Application.ProjectManagementApps.Queries.GetProjectManagementApp;
using EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagement;
using EcoLink.Application.ProjectManagementApps.Commands.UpdateProjectManagement;

namespace EcoLink.WebApi.Controllers.ProjectManagement;

public class ProjectManagementAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(ProjectManagementAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateProjectManagementWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new CreateProjectManagementWithReturnCommand(command), cancellationToken) });

    [HttpPut("update-status")]
    [ProducesResponseType(typeof(ProjectManagementAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStatus(UpdateProjectManagementStatusCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(command, cancellationToken) });

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(ProjectManagementAppResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetProjectManagementAppQuery(id), cancellationToken) });

    [HttpGet("get-all-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<ProjectManagementAppResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetAllProjectManagementAppsByUserIdQuery(userId), cancellationToken) });
}
