using MediatR;
using OrgBloom.Application.Interfaces;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Queries.GetInvestors;

public record GetAllInvestorsQuery : IRequest<IEnumerable<Investor>> { }

public class GetAllInvestorsQueryHandler(IRepository<Investor> repository) : IRequestHandler<GetAllInvestorsQuery, IEnumerable<Investor>>
{
    public async Task<IEnumerable<Investor>> Handle(GetAllInvestorsQuery request, CancellationToken cancellationToken)
    => await Task.Run(() => repository.SelectAll().ToList());
}
