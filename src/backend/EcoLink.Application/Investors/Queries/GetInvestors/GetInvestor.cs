using EcoLink.Application.Investors.DTOs;

namespace EcoLink.Application.Investors.Queries.GetInvestors;

public record GetInvestorQuery : IRequest<InvestorResultDto>
{
    public GetInvestorQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetInvestorQueryHandler(IRepository<Investor> repository, IMapper mapper) : 
    IRequestHandler<GetInvestorQuery, InvestorResultDto>
{
    public async Task<InvestorResultDto> Handle(GetInvestorQuery request, CancellationToken cancellationToken)
        => mapper.Map<InvestorResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
