namespace EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerCommand : IRequest<int>
{
    public UpdateProjectManagerCommand(UpdateProjectManagerCommand command)
    {
        Id = command.Id;
        Area = command.Area;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Expectation = command.Expectation;
        IsSubmitted = command.IsSubmitted;
        ProjectDirection = command.ProjectDirection;
    }

    public long Id { get; set; }
    public string Area { get; set; } = string.Empty;
    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public long UserId { get; set; }
}

public class UpdateProjectManagerCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : 
    IRequestHandler<UpdateProjectManagerCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}