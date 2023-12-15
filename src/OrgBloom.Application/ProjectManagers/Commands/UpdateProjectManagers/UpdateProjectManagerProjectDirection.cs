using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerProjectDirectionCommand : IRequest<int>
{
    public UpdateProjectManagerProjectDirectionCommand(UpdateProjectManagerProjectDirectionCommand command)
    {
        Id = command.Id;
        ProjectDirection = command.Area;
    }

    public long Id { get; set; }
    public string ProjectDirection { get; set; } = string.Empty;
}

public class UpdateProjectManagerAreaCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerProjectDirectionCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerProjectDirectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM Area");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}