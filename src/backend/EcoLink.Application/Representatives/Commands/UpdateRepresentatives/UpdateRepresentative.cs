namespace EcoLink.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeCommand : IRequest<int>
{
    public UpdateRepresentativeCommand(UpdateRepresentativeCommand command)
    {
        Id = command.Id;
        Area = command.Area;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Expectation = command.Expectation;
        IsSubmitted = command.IsSubmitted;
    }

    public long Id { get; set; }
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public long UserId { get; set; }
}

public class UpdateRepresentativeCommandHandler(IRepository<Representative> repository, IMapper mapper) : 
    IRequestHandler<UpdateRepresentativeCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}