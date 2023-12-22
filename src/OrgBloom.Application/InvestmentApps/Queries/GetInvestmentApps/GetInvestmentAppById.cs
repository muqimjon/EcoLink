using OrgBloom.Application.InvestmentApps.DTOs;

namespace OrgBloom.Application.InvestmentApps.Queries.GetInvestmentApp;

public record GetInvestmentAppByIdQuery : IRequest<InvestmentAppResultDto>
{
    public GetInvestmentAppByIdQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetInvestmentAppByIdQueryHendler(IRepository<InvestmentApp> repository, IMapper mapper) : 
    IRequestHandler<GetInvestmentAppByIdQuery, InvestmentAppResultDto>
{
    public async Task<InvestmentAppResultDto> Handle(GetInvestmentAppByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));
        return mapper.Map<InvestmentAppResultDto>(entity);
    }
}
