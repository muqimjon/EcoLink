namespace EcoLink.Application.Entrepreneurs.Commands.DeleteEntrepreneurs;

public record DeleteEntrepreneurCommand : IRequest<bool>
{
    public DeleteEntrepreneurCommand(DeleteEntrepreneurCommand command) { Id = command.Id; }
    public long Id { get; set; }
}

public class DeleteEntrepreneurCommandHandler(IRepository<Entrepreneur> repository) : 
    IRequestHandler<DeleteEntrepreneurCommand, bool>
{
    public async Task<bool> Handle(DeleteEntrepreneurCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | delete Entrepreneur");

        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}