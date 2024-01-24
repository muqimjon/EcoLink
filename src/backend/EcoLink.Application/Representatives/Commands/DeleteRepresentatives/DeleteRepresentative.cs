namespace EcoLink.Application.Representatives.Commands.DeleteRepresentatives;

public record DeleteRepresentativeCommand : IRequest<bool>
{
    public DeleteRepresentativeCommand(long id) { Id = id; }
    public long Id { get; set; }
}

public class DeleteRepresentativeCommandHandler(IRepository<Representative> repository) : 
    IRequestHandler<DeleteRepresentativeCommand, bool>
{
    public async Task<bool> Handle(DeleteRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representation is not found with id: {request.Id} | delete representative");

        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}