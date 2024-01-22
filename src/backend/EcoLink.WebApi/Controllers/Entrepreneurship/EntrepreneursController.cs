using EcoLink.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.DeleteEntrepreneurs;

namespace EcoLink.WebApi.Controllers.Entrepreneurship;

public class EntrepreneursController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEntrepreneurCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurCommand(command)));

    [HttpPost("create-with-return")]
    public async Task<IActionResult> Create(CreateEntrepreneurshipAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand(command)));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateEntrepreneurCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurCommand(command)));

    [HttpPut("update-assets-invested")]
    public async Task<IActionResult> Update(UpdateEntrepreneurAssetsInvestedByUserIdCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurAssetsInvestedByUserIdCommand(command)));

    [HttpPut("update-help-type")]
    public async Task<IActionResult> Update(UpdateEntrepreneurHelpTypeByUserIdCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurHelpTypeByUserIdCommand(command)));

    [HttpPut("update-submittion")]
    public async Task<IActionResult> Update(UpdateEntrepreneurIsSubmittedByUserIdCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurIsSubmittedByUserIdCommand(command)));

    [HttpPut("update-poject")]
    public async Task<IActionResult> Update(UpdateEntrepreneurProjectByUserIdCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurProjectByUserIdCommand(command)));

    [HttpPut("update-required-funding")]
    public async Task<IActionResult> Update(UpdateEntrepreneurRequiredFundingByUserIdCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurRequiredFundingByUserIdCommand(command)));

    [HttpPut("update-sector")]
    public async Task<IActionResult> Update(UpdateEntrepreneurSectorByUserIdCommand command)
        => Ok(await mediator.Send(new UpdateEntrepreneurSectorByUserIdCommand(command)));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteEntrepreneurCommand command)
        => Ok(await mediator.Send(new DeleteEntrepreneurCommand(command)));

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] GetEntrepreneurQuery command)
        => Ok(await mediator.Send(new GetEntrepreneurQuery(command)));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new GetAllEntrepreneursQuery()));
}
