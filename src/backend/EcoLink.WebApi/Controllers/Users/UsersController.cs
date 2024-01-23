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
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateUserCommand(command), cancellationToken));

    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateUserWithReturnCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateUserWithReturnCommand(command), cancellationToken));

    [HttpPut("update")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateUserCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new UpdateUserCommand(command), cancellationToken));

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new DeleteUserCommand(id), cancellationToken));

    [HttpGet("get/{userId:long}")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long userId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUserQuery(userId), cancellationToken));

    [HttpGet("get-by-telegram-id/{telegramId:long}")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTelegramId(long telegramId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUserByTelegramIdQuery(telegramId), cancellationToken));

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<UserResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetAllUsersQuery(), cancellationToken));
}
