using MediatR;
using OrgBloom.Application.Interfaces;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Queries.GetInvestors;

public record InvestorGetAllCommand : IRequest<IEnumerable<Investor>>
{
}

public class InvestorGetAllCommandHandler : IRequestHandler<InvestorGetAllCommand, IEnumerable<Investor>>
{
    private readonly IRepository<Investor> repository;

    public async Task<IEnumerable<Investor>> Handle(InvestorGetAllCommand request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => repository.SelectAll().ToList());
    }
}
