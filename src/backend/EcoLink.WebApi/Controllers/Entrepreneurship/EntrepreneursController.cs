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
    public async Task<IActionResult> Create(CreateEntrepreneurCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurCommand(command)));

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(EntrepreneurResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateEntrepreneurshipAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand(command)));

    [HttpPut("update")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateEntrepreneurCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurCommand(command)));

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id)
        => Ok(await mediator.Send(new DeleteEntrepreneurCommand(id)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(EntrepreneurResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id)
        => Ok(await mediator.Send(new GetEntrepreneurQuery(id)));

    [HttpGet("get-by-userId/{userId:long}")]
    [ProducesResponseType(typeof(EntrepreneurResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(long userId)
        => Ok(await mediator.Send(new GetEntrepreneurByUserIdQuery(userId)));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<EntrepreneurResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllEntrepreneursQuery()));
}
