using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Investors.Queries.GetInvestors;

public record GetInvestorByIdQuery : IRequest<InvestorResultDto>
{
    public GetInvestorByIdQuery(GetInvestorByIdQuery command) { Id = command.Id; }
    public int Id { get; set; }
}

public class GetInvestorQueryHendler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<GetInvestorByIdQuery, InvestorResultDto>
{
    public async Task<InvestorResultDto> Handle(GetInvestorByIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<InvestorResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
