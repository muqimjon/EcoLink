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
    public async Task<IActionResult> Create(CreateUserWithReturnCommand command)
        => Ok(await mediator.Send(new CreateUserWithReturnCommand(command)));

    [HttpPut("update")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateUserCommand command)
        => Ok(await mediator.Send(new UpdateUserCommand(command)));

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id)
        => Ok(await mediator.Send(new DeleteUserCommand(id)));

    [HttpGet("get/{userId:long}")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long userId)
        => Ok(await mediator.Send(new GetUserQuery(userId)));

    [HttpGet("get-by-telegram-id/{telegramId:long}")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTelegramId(long telegramId)
        => Ok(await mediator.Send(new GetUserByTelegramIdQuery(telegramId)));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<UserResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication()
        => Ok(await mediator.Send(new GetAllUsersQuery()));
}
