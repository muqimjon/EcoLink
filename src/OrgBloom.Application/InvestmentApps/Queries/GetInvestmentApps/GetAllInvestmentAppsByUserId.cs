using OrgBloom.Domain.Entities.Investment;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.InvestmentApps.DTOs;

namespace OrgBloom.Application.InvestmentApps.Queries.GetInvestmentApp;

public record GetAllInvestmentAppByUserIdQuery : IRequest<IEnumerable<InvestmentAppResultDto>>
{
    public GetAllInvestmentAppByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllInvestmentAppByUserIdQueryHendler(IRepository<InvestmentApp> repository, IMapper mapper) : IRequestHandler<GetAllInvestmentAppByUserIdQuery, IEnumerable<InvestmentAppResultDto>>
{
    public async Task<IEnumerable<InvestmentAppResultDto>> Handle(GetAllInvestmentAppByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(entity => entity.UserId.Equals(request.UserId));
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<InvestmentAppResultDto>>(entities);
    }
}
