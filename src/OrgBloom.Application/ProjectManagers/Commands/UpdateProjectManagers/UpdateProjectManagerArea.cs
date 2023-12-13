using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerAreaCommand : IRequest<int>
{
    public UpdateProjectManagerAreaCommand(UpdateProjectManagerCommand command)
    {
        Id = command.Id;
        Area = command.Area;
    }

    public long Id { get; set; }
    public string Area { get; set; } = string.Empty;
}

public class UpdateProjectManagerAreaCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerAreaCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerAreaCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM Area");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}