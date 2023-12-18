using AutoMapper;
using OrgBloom.Application.Commons.Helpers;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.ProjectManagement;

namespace OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;

public record CreateProjectManagerCommand : IRequest<int>
{
    public CreateProjectManagerCommand(CreateProjectManagerCommand command)
    {
        UserId = command.UserId;
        Purpose = command.Purpose;
        Expectation = command.Expectation;
        IsSubmitted = command.IsSubmitted;
        ProjectDirection = command.ProjectDirection;
    }

    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class CreateProjectManagerCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<CreateProjectManagerCommand, int>
{
    public async Task<int> Handle(CreateProjectManagerCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"PM is already exist with user id: {request.UserId} | create project manager");

        entity = mapper.Map<ProjectManager>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        return await repository.SaveAsync();
    }
}