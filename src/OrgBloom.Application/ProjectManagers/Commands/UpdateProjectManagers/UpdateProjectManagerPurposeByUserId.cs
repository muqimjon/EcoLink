using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.ProjectManagement;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerPurposeVyUserIdCommand : IRequest<int>
{
    public UpdateProjectManagerPurposeVyUserIdCommand(UpdateProjectManagerPurposeVyUserIdCommand command)
    {
        UserId = command.UserId;
        Purpose = command.Purpose;
    }

    public long UserId { get; set; }
    public string Purpose { get; set; } = string.Empty;
}

public class UpdateProjectManagerPurposeByUserIdCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerPurposeVyUserIdCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerPurposeVyUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"PM is not found with id: {request.UserId} | update PM Purpose");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}