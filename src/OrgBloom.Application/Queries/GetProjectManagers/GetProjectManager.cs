using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;
using OrgBloom.Application.DTOs.ProjectManagers;

namespace OrgBloom.Application.Queries.GetProjectManagers;

public record GetProjectManagerQuery : IRequest<ProjectManagerResultDto>
{
    public GetProjectManagerQuery(GetProjectManagerQuery command) { Id = command.Id; }
    public int Id { get; set; }
}

public class GetProjectManagerQueryHendler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<GetProjectManagerQuery, ProjectManagerResultDto>
{
    public async Task<ProjectManagerResultDto> Handle(GetProjectManagerQuery request, CancellationToken cancellationToken)
        => mapper.Map<ProjectManagerResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
