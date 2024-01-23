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
    public async Task<IActionResult> Delete([FromQuery] DeleteEntrepreneurCommand command)
        => Ok(await mediator.Send(new DeleteEntrepreneurCommand(command)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(EntrepreneurResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetEntrepreneurQuery command)
        => Ok(await mediator.Send(new GetEntrepreneurQuery(command)));

    [HttpGet("get-all-by-user-id/{user-id:long}")]
    [ProducesResponseType(typeof(IEnumerable<EntrepreneurResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId([FromQuery] GetEntrepreneurByUserIdQuery query)
        => Ok(await mediator.Send(new GetEntrepreneurByUserIdQuery(query.UserId)));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<EntrepreneurResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllEntrepreneursQuery()));
}
