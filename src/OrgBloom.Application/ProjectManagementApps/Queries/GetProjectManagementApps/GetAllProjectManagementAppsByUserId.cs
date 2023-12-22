using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.ProjectManagement;
using OrgBloom.Application.ProjectManagementApps.DTOs;

namespace OrgBloom.Application.ProjectManagementApps.Queries.GetProjectManagementApp;

public record GetAllProjectManagementAppByUserIdQuery : IRequest<IEnumerable<ProjectManagementAppResultDto>>
{
    public GetAllProjectManagementAppByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllProjectManagementAppByUserIdQueryHendler(IRepository<ProjectManagementApp> repository, IMapper mapper) : IRequestHandler<GetAllProjectManagementAppByUserIdQuery, IEnumerable<ProjectManagementAppResultDto>>
{
    public async Task<IEnumerable<ProjectManagementAppResultDto>> Handle(GetAllProjectManagementAppByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(entity => entity.UserId.Equals(request.UserId));
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<ProjectManagementAppResultDto>>(entities);
    }
}
