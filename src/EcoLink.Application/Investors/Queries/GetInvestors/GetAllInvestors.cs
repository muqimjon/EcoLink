using EcoLink.Application.Investors.DTOs;

namespace EcoLink.Application.Investors.Queries.GetInvestors;

public record GetAllInvestorsQuery : IRequest<IEnumerable<InvestorResultDto>> { }

public class GetAllInvestorsQueryHandler(IRepository<Investor> repository, IMapper mapper) : 
    IRequestHandler<GetAllInvestorsQuery, IEnumerable<InvestorResultDto>>
{
    public async Task<IEnumerable<InvestorResultDto>> Handle(GetAllInvestorsQuery request, CancellationToken cancellationToken)
    => await Task.Run(() => mapper.Map<IEnumerable<InvestorResultDto>>(repository.SelectAll().ToList()));
}
