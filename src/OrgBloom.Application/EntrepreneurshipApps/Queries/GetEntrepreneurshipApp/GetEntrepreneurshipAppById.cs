using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.ProjectManagement;
using OrgBloom.Application.ProjectManagementApps.DTOs;

namespace OrgBloom.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

public record GetEntrepreneurshipAppByIdCommand : IRequest<ProjectManagementAppResultDto>
{
    public GetEntrepreneurshipAppByIdCommand(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetEntrepreneurshipAppByIdCommandHendler(IRepository<ProjectManagementApp> repository, IMapper mapper) : IRequestHandler<GetEntrepreneurshipAppByIdCommand, ProjectManagementAppResultDto>
{
    public async Task<ProjectManagementAppResultDto> Handle(GetEntrepreneurshipAppByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));

        return mapper.Map<ProjectManagementAppResultDto>(entity);
    }
}
