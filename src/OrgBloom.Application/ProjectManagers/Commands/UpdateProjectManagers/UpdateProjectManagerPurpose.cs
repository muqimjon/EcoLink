using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerPurposeCommand : IRequest<int>
{
    public UpdateProjectManagerPurposeCommand(UpdateProjectManagerPurposeCommand command)
    {
        Id = command.Id;
        Purpose = command.Purpose;
    }

    public long Id { get; set; }
    public string Purpose { get; set; } = string.Empty;
}

public class UpdateProjectManagerPurposeCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerPurposeCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerPurposeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM Purpose");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}