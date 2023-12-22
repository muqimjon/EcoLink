﻿using OrgBloom.Application.ProjectManagementApps.DTOs;

namespace OrgBloom.Application.ProjectManagementApps.Commands.CreateProjectManagementApps;

public record CreateProjectManagementAppWithReturnCommand : IRequest<ProjectManagementAppResultDto>
{
    public CreateProjectManagementAppWithReturnCommand(CreateProjectManagementAppWithReturnCommand command)
    {
        Degree = command.Degree;
        Purpose = command.Purpose;
        Address = command.Address;
        LastName = command.LastName;
        FirstName = command.FirstName;
        Languages = command.Languages;
        Experience = command.Experience;
        DateOfBirth = command.DateOfBirth;
        Expectation = command.Expectation;
        ProjectDirection = command.ProjectDirection;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
}

public class CreateProjectManagementAppWithReturnCommandHandler(IRepository<ProjectManagementApp> repository, IMapper mapper) : 
    IRequestHandler<CreateProjectManagementAppWithReturnCommand, ProjectManagementAppResultDto>
{
    public async Task<ProjectManagementAppResultDto> Handle(CreateProjectManagementAppWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ProjectManagementApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<ProjectManagementAppResultDto>(entity);
    }
}