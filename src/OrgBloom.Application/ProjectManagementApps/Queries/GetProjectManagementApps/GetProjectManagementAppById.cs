using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.ProjectManagement;
using OrgBloom.Application.ProjectManagementApps.DTOs;

namespace OrgBloom.Application.ProjectManagementApps.Queries.GetProjectManagementApp;

public record GetProjectManagementAppByIdCommand : IRequest<ProjectManagementAppResultDto>
{
    public GetProjectManagementAppByIdCommand(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetProjectManagementAppByIdCommandHendler(IRepository<ProjectManagementApp> repository, IMapper mapper) : IRequestHandler<GetProjectManagementAppByIdCommand, ProjectManagementAppResultDto>
{
    public async Task<ProjectManagementAppResultDto> Handle(GetProjectManagementAppByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));
        return mapper.Map<ProjectManagementAppResultDto>(entity);
    }
}
