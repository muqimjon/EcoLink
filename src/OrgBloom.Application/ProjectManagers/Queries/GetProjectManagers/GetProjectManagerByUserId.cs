using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Domain.Entities.ProjectManagement;

namespace OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;

public record GetProjectManagerByUserIdQuery : IRequest<ProjectManagerResultDto>
{
    public GetProjectManagerByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetProjectManagerByUserIdQueryHendler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<GetProjectManagerByUserIdQuery, ProjectManagerResultDto>
{
    public async Task<ProjectManagerResultDto> Handle(GetProjectManagerByUserIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<ProjectManagerResultDto>(await repository.SelectAsync(i => i.UserId.Equals(request.UserId), includes: ["User"]));
}
