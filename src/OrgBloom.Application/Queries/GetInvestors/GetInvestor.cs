using MediatR;
using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;
using OrgBloom.Application.DTOs.Investors;

namespace OrgBloom.Application.Queries.GetInvestors;

public record GetInvestorQuery : IRequest<InvestorResultDto>
{
    public GetInvestorQuery(GetInvestorQuery command) { Id = command.Id; }
    public int Id { get; set; }
}

public class GetInvestorQueryHendler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<GetInvestorQuery, InvestorResultDto>
{
    public async Task<InvestorResultDto> Handle(GetInvestorQuery request, CancellationToken cancellationToken)
        => mapper.Map<InvestorResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
