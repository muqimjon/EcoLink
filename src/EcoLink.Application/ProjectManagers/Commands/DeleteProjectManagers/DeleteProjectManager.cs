namespace EcoLink.Application.ProjectManagers.Commands.DeleteProjectManagers;

public record DeleteProjectManagerCommand : IRequest<bool>
{
    public DeleteProjectManagerCommand(DeleteProjectManagerCommand command) { Id = command.Id; }
    public long Id { get; set; }
}

public class DeleteProjectManagerCommandHandler(IRepository<ProjectManager> repository) : 
    IRequestHandler<DeleteProjectManagerCommand, bool>
{
    public async Task<bool> Handle(DeleteProjectManagerCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new AlreadyExistException($"PM is already exist with user id: {request.Id} | delete project manager");

        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}