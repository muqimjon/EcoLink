using OrgBloom.Application.ProjectManagers.DTOs;

namespace OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;

public record GetAllProjectManagersQuery : IRequest<IEnumerable<ProjectManagerResultDto>> { }

public class GetAllProjectManagersQueryHandler(IRepository<ProjectManager> repository, IMapper mapper) : 
    IRequestHandler<GetAllProjectManagersQuery, IEnumerable<ProjectManagerResultDto>>
{
    public async Task<IEnumerable<ProjectManagerResultDto>> Handle(GetAllProjectManagersQuery request, CancellationToken cancellationToken)
    => await Task.Run(() => mapper.Map<IEnumerable<ProjectManagerResultDto>>(repository.SelectAll().ToList()));
}
