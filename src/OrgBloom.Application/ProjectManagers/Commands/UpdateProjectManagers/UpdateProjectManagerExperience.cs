﻿using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerExperienceCommand : IRequest<int>
{
    public UpdateProjectManagerExperienceCommand(UpdateProjectManagerCommand command)
    {
        Id = command.Id;
        Experience = command.Experience;
    }

    public long Id { get; set; }
    public string Experience { get; set; } = string.Empty;
}

public class UpdateProjectManagerExperienceCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerExperienceCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM Experience");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}