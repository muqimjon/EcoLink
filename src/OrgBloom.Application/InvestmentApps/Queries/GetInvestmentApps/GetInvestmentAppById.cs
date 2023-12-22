using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.ProjectManagement;
using OrgBloom.Application.ProjectManagementApps.DTOs;

namespace OrgBloom.Application.InvestmentApps.Queries.GetInvestmentApp;

public record GetInvestmentAppByIdCommand : IRequest<ProjectManagementAppResultDto>
{
    public GetInvestmentAppByIdCommand(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetInvestmentAppByIdCommandHendler(IRepository<ProjectManagementApp> repository, IMapper mapper) : IRequestHandler<GetInvestmentAppByIdCommand, ProjectManagementAppResultDto>
{
    public async Task<ProjectManagementAppResultDto> Handle(GetInvestmentAppByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));
        return mapper.Map<ProjectManagementAppResultDto>(entity);
    }
}
