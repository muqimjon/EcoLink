using EcoLink.Application.Users.DTOs;
using EcoLink.Application.Users.Queries.GetUsers;
using EcoLink.Application.Users.Commands.CreateUsers;
using EcoLink.Application.Users.Commands.DeleteUsers;
using EcoLink.Application.Users.Commands.UpdateUsers;

namespace EcoLink.WebApi.Controllers.Users;

public class UsersController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateUserCommand command)
        => Ok(await mediator.Send(new CreateUserCommand(command)));

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateUserWithReturnTgResultCommand command)
        => Ok(await mediator.Send(new CreateUserWithReturnTgResultCommand(command)));

    [HttpPut("update")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateUserCommand command)
        => Ok(await mediator.Send(new UpdateUserCommand(command)));

    [HttpDelete("delete")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] DeleteUserCommand command)
        => Ok(await mediator.Send(new DeleteUserCommand(command)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetUserByIdQuery query)
        => Ok(await mediator.Send(new GetUserByIdQuery(query)));

    [HttpGet("get-by-telegram-id/{telegram-id:long}")]
    [ProducesResponseType(typeof(UserTelegramResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTelegramId([FromRoute] GetUserByTelegramIdQuery query)
        => Ok(await mediator.Send(new GetUserByTelegramIdQuery(query.TelegramId)));

    [HttpGet("get-for-application/{id:long}")]
    [ProducesResponseType(typeof(UserApplyResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication([FromRoute] GetUserForApplicationQuery query)
        => Ok(await mediator.Send(new GetUserForApplicationQuery(query.Id)));
}
