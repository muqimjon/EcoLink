using EcoLink.Application.Users.Queries.GetUsers;
using EcoLink.Application.Users.Commands.CreateUsers;
using EcoLink.Application.Users.Commands.DeleteUsers;
using EcoLink.Application.Users.Commands.UpdateUsers;

namespace EcoLink.WebApi.Controllers.Users;

public class UsersController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateUserCommand command)
        => Ok(await mediator.Send(new CreateUserCommand(command)));

    [HttpPost("create-with-return")]
    public async Task<IActionResult> Create(CreateUserWithReturnTgResultCommand command)
        => Ok(await mediator.Send(new CreateUserWithReturnTgResultCommand(command)));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateUserCommand command)
        => Ok(await mediator.Send(new UpdateUserCommand(command)));

    [HttpPut("update-address")]
    public async Task<IActionResult> Update(UpdateAddressCommand command)
        => Ok(await mediator.Send(new UpdateAddressCommand(command)));

    [HttpPut("update-age")]
    public async Task<IActionResult> Update(UpdateAgeCommand command)
        => Ok(await mediator.Send(new UpdateAgeCommand(command)));

    [HttpPut("update-date-of-birth")]
    public async Task<IActionResult> Update(UpdateDateOfBirthCommand command)
        => Ok(await mediator.Send(new UpdateDateOfBirthCommand(command)));

    [HttpPut("update-degree")]
    public async Task<IActionResult> Update(UpdateDegreeCommand command)
        => Ok(await mediator.Send(new UpdateDegreeCommand(command)));

    [HttpPut("update-email")]
    public async Task<IActionResult> Update(UpdateEmailCommand command)
        => Ok(await mediator.Send(new UpdateEmailCommand(command)));

    [HttpPut("update-experience")]
    public async Task<IActionResult> Update(UpdateExperienceCommand command)
        => Ok(await mediator.Send(new UpdateExperienceCommand(command)));

    [HttpPut("update-first-name")]
    public async Task<IActionResult> Update(UpdateFirstNameCommand command)
        => Ok(await mediator.Send(new UpdateFirstNameCommand(command)));

    [HttpPut("update-last-name")]
    public async Task<IActionResult> Update(UpdateLastNameCommand command)
        => Ok(await mediator.Send(new UpdateLastNameCommand(command)));

    [HttpPut("update-languages")]
    public async Task<IActionResult> Update(UpdateLanguagesCommand command)
        => Ok(await mediator.Send(new UpdateLanguagesCommand(command)));

    [HttpPut("update-language-code")]
    public async Task<IActionResult> Update(UpdateLanguageCodeCommand command)
        => Ok(await mediator.Send(new UpdateLanguageCodeCommand(command)));

    [HttpPut("update-patronomyc")]
    public async Task<IActionResult> Update(UpdatePatronomycCommand command)
        => Ok(await mediator.Send(new UpdatePatronomycCommand(command)));

    [HttpPut("update-phone")]
    public async Task<IActionResult> Update(UpdatePhoneCommand command)
        => Ok(await mediator.Send(new UpdatePhoneCommand(command)));

    [HttpPut("update-profession")]
    public async Task<IActionResult> Update(UpdateProfessionCommand command)
        => Ok(await mediator.Send(new UpdateProfessionCommand(command)));

    [HttpPut("update-state")]
    public async Task<IActionResult> Update(UpdateStateCommand command)
        => Ok(await mediator.Send(new UpdateStateCommand(command.Id, command.State)));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteUserCommand command)
        => Ok(await mediator.Send(new DeleteUserCommand(command)));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> Get([FromQuery] GetUserByIdQuery command)
        => Ok(await mediator.Send(new GetUserByIdQuery(command)));

    [HttpGet("get-all/{id:long}")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllUsersQuery()));

    [HttpGet("grt-address/{id:long}")]
    public async Task<IActionResult> GetAddress([FromQuery] GetAddressQuery command)
        => Ok(await mediator.Send(new GetAddressQuery(command.Id)));

    [HttpGet("get-age/{id:long}")]
    public async Task<IActionResult> GetAge([FromQuery] GetAgeQuery command)
        => Ok(await mediator.Send(new GetAgeQuery(command.Id)));

    [HttpGet("get-date-of-birth/{id:long}")]
    public async Task<IActionResult> GetDateOfBirth([FromQuery] GetDateOfBirthQuery command)
        => Ok(await mediator.Send(new GetDateOfBirthQuery(command.Id)));

    [HttpGet("get-Email/{id:long}")]
    public async Task<IActionResult> GetEmail([FromQuery] GetEmailQuery command)
        => Ok(await mediator.Send(new GetEmailQuery(command.Id)));

    [HttpGet("get-experience/{id:long}")]
    public async Task<IActionResult> GetExperience([FromQuery] GetExperienceQuery command)
            => Ok(await mediator.Send(new GetExperienceQuery(command.Id)));

    [HttpGet("get-language-code/{id:long}")]
    public async Task<IActionResult> GetLanguageCode([FromQuery] GetLanguageCodeQuery command)
        => Ok(await mediator.Send(new GetLanguageCodeQuery(command.Id)));

    [HttpGet("get-languages/{id:long}")]
    public async Task<IActionResult> GetLanguages([FromQuery] GetLanguagesQuery command)
        => Ok(await mediator.Send(new GetLanguagesQuery(command.Id)));

    [HttpGet("get-profession/{id:long}")]
    public async Task<IActionResult> GetProfession([FromQuery] GetProfessionQuery command)
        => Ok(await mediator.Send(new GetProfessionQuery(command.Id)));

    [HttpGet("get-state/{id:long}")]
    public async Task<IActionResult> GetState([FromQuery] GetStateQuery command)
        => Ok(await mediator.Send(new GetStateQuery(command.Id)));

    [HttpGet("get-by-telegram-id/{id:long}")]
    public async Task<IActionResult> GetByTelegramId([FromRoute] GetUserByTelegramIdQuery command)
        => Ok(await mediator.Send(new GetUserByTelegramIdQuery(command.TelegramId)));
}
