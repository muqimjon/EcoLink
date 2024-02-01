namespace EcoLink.Application.RepresentationApps.Commands.UpdateRepresentation;

public record UpdateRepresentationStatusCommand : IRequest<RepresentationAppResultDto>
{
    public UpdateRepresentationStatusCommand(UpdateRepresentationStatusCommand command)
    {
        UserId = command.UserId;
        IsOld = command.IsOld;
    }

    public long UserId { get; set; }
    public bool IsOld { get; set; } = true;
}

public class UpdateRepresentationStatusCommandHandler(IMapper mapper, IRepository<RepresentationApp> repository) :
    IRequestHandler<UpdateRepresentationStatusCommand, RepresentationAppResultDto>
{
    public async Task<RepresentationAppResultDto> Handle(UpdateRepresentationStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = repository.SelectAll(e => e.UserId == request.UserId).OrderBy(e => e.Id).LastOrDefault() ??
            throw new NotFoundException($"{nameof(RepresentationApp)} is not found with {nameof(request.UserId)} = {request.UserId}");

        mapper.Map(request, entity);
        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<RepresentationAppResultDto>(entity);
    }
}
