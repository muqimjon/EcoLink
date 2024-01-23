using EcoLink.Application.Entrepreneurs.DTOs;
using EcoLink.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.DeleteEntrepreneurs;

namespace EcoLink.WebApi.Controllers.Entrepreneurship;

public class EntrepreneursController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateEntrepreneurCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateEntrepreneurCommand(command), cancellationToken));

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(EntrepreneurResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateEntrepreneurshipAppWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand(command), cancellationToken));

    [HttpPut("update")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateEntrepreneurCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new UpdateEntrepreneurCommand(command), cancellationToken));

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new DeleteEntrepreneurCommand(id), cancellationToken));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(EntrepreneurResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetEntrepreneurQuery(id), cancellationToken));

    [HttpGet("get-by-userId/{userId:long}")]
    [ProducesResponseType(typeof(EntrepreneurResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(long userId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetEntrepreneurByUserIdQuery(userId), cancellationToken));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<EntrepreneurResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetAllEntrepreneursQuery(), cancellationToken));
}
