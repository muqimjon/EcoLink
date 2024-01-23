using EcoLink.Application.ProjectManagers.DTOs;
using EcoLink.Application.ProjectManagementApps.Queries.GetProjectManagementApp;
using EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagementApps;

namespace EcoLink.WebApi.Controllers.ProjectManagement;

public class ProjectManagementAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateProjectManagementAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateProjectManagementAppWithReturnCommand(command)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(ProjectManagerResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id)
        => Ok(await mediator.Send(new GetProjectManagementAppQuery(id)));

    [HttpGet("get-all-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<ProjectManagerResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId)
        => Ok(await mediator.Send(new GetAllProjectManagementAppsByUserIdQuery(userId)));
}
