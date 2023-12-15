using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Application.ProjectManagers.DTOs;

namespace OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;

public record CreateProjectManagerWithReturnCommand : IRequest<ProjectManagerResultDto>
{
    public CreateProjectManagerWithReturnCommand(CreateProjectManagerWithReturnCommand command)
    {
        Area = command.Area;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Address = command.Address;
        Languages = command.Languages;
        Experience = command.Experience;
        Expectation = command.Expectation;
        IsSubmitted = command.IsSubmitted;
    }

    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
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