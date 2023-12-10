using MediatR;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Queries.GetInvestors;

public record GetInvestorQuery : IRequest<Investor>
{
    public GetInvestorQuery(GetInvestorQuery command)
    {
        Id = command.Id;
    }

    public int Id { get; set; }
}

public class GetInvestorQueryHendler(IRepository<Investor> repository) : IRequestHandler<GetInvestorQuery, Investor>
{
    public async Task<Investor> Handle(GetInvestorQuery request, CancellationToken cancellationToken)
        => await repository.SelectAsync(i => i.Id.Equals(request.Id));
}
