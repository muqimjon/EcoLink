using EcoLink.Application.InvestmentApps.DTOs;

namespace EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;

public record GetInvestmentAppQuery : IRequest<InvestmentAppResultDto>
{
    public GetInvestmentAppQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetInvestmentAppQueryHandler(IRepository<InvestmentApp> repository, IMapper mapper) : 
    IRequestHandler<GetInvestmentAppQuery, InvestmentAppResultDto>
{
    public async Task<InvestmentAppResultDto> Handle(GetInvestmentAppQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));
        return mapper.Map<InvestmentAppResultDto>(entity);
    }
}
