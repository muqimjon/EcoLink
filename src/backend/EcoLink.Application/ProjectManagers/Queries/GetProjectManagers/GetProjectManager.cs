using EcoLink.Application.ProjectManagers.DTOs;

namespace EcoLink.Application.ProjectManagers.Queries.GetProjectManagers;

public record GetProjectManagerQuery : IRequest<ProjectManagerResultDto>
{
    public GetProjectManagerQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetProjectManagerQueryHandler(IRepository<ProjectManager> repository, IMapper mapper) : 
    IRequestHandler<GetProjectManagerQuery, ProjectManagerResultDto>
{
    public async Task<ProjectManagerResultDto> Handle(GetProjectManagerQuery request, CancellationToken cancellationToken)
        => mapper.Map<ProjectManagerResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
