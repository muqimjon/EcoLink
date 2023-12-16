using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Domain.Entities.ProjectManagement;

namespace OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;

public record CreateProjectManagerWithReturnCommand : IRequest<ProjectManagerResultDto>
{
    public CreateProjectManagerWithReturnCommand(CreateProjectManagerWithReturnCommand command)
    {
        ProjectDirection = command.ProjectDirection;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Expectation = command.Expectation;
        IsSubmitted = command.IsSubmitted;
    }

    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class CreateProjectManagerWithReturnCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<CreateProjectManagerWithReturnCommand, ProjectManagerResultDto>
{
    public async Task<ProjectManagerResultDto> Handle(CreateProjectManagerWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"ProjectManager is already exist create ProjectManager by user id {request.UserId}");

        entity = mapper.Map<ProjectManager>(request);
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<ProjectManagerResultDto>(entity);
    }
}