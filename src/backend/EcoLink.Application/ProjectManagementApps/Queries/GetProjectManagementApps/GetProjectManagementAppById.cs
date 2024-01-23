using EcoLink.Application.ProjectManagementApps.DTOs;

namespace EcoLink.Application.ProjectManagementApps.Queries.GetProjectManagementApp;

public record GetProjectManagementAppQuery : IRequest<ProjectManagementAppResultDto>
{
    public GetProjectManagementAppQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetProjectManagementAppQueryHendler(IRepository<ProjectManagementApp> repository, IMapper mapper) : 
    IRequestHandler<GetProjectManagementAppQuery, ProjectManagementAppResultDto>
{
    public async Task<ProjectManagementAppResultDto> Handle(GetProjectManagementAppQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));
        return mapper.Map<ProjectManagementAppResultDto>(entity);
    }
}
