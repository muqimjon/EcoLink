namespace EcoLink.Application.EntrepreneurshipApps.Commands.UpdateEntrepreneurship;

public record UpdateEntrepreneurshipStatusCommand : IRequest<EntrepreneurshipAppResultDto>
{
    public UpdateEntrepreneurshipStatusCommand(UpdateEntrepreneurshipStatusCommand command)
    {
        UserId = command.UserId;
    }

    public long UserId { get; set; }
    public bool IsOld { get; set; } = true;
}

public class UpdateEntrepreneurshipStatusCommandHandler(IMapper mapper, IRepository<EntrepreneurshipApp> repository) : 
    IRequestHandler<UpdateEntrepreneurshipStatusCommand, EntrepreneurshipAppResultDto>
{
    public async Task<EntrepreneurshipAppResultDto> Handle(UpdateEntrepreneurshipStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(e => e.UserId == request.UserId) ??
            throw new NotFoundException($"{nameof(EntrepreneurshipApp)} is not found with {nameof(request.UserId)} = {request.UserId}");

        mapper.Map(request, entity);
        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<EntrepreneurshipAppResultDto>(entity);
    }
}
