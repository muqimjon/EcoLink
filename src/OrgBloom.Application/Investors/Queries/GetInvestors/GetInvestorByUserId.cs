using AutoMapper;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Investment;

namespace OrgBloom.Application.Investors.Queries.GetInvestors;

public record GetInvestorByUserIdQuery : IRequest<InvestorResultDto>
{
    public GetInvestorByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetInvestorByUserIdQueryHendler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<GetInvestorByUserIdQuery, InvestorResultDto>
{
    public async Task<InvestorResultDto> Handle(GetInvestorByUserIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<InvestorResultDto>(await repository.SelectAsync(i => i.UserId.Equals(request.UserId), includes: ["User"]));
}
