using EcoLink.Application.InvestmentApps.DTOs;

namespace EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;

public record GetAllInvestmentAppsByUserIdQuery : IRequest<IEnumerable<InvestmentAppResultDto>>
{
    public GetAllInvestmentAppsByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllInvestmentAppsByUserIdQueryHandler(IRepository<InvestmentApp> repository, IMapper mapper) :
    IRequestHandler<GetAllInvestmentAppsByUserIdQuery, IEnumerable<InvestmentAppResultDto>>
{
    public async Task<IEnumerable<InvestmentAppResultDto>> Handle(GetAllInvestmentAppsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(entity => entity.UserId.Equals(request.UserId), includes: ["User"]);
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<InvestmentAppResultDto>>(entities);
    }
}
