namespace OrgBloom.Application.Investors.Queries.GetInvestors;

public record GetInvestmentAmountByUserIdQuery : IRequest<string>
{
    public GetInvestmentAmountByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetInvestmentAmountByUserIdQueryHendler(IRepository<Investor> repository) : 
    IRequestHandler<GetInvestmentAmountByUserIdQuery, string>
{
    public async Task<string> Handle(GetInvestmentAmountByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).InvestmentAmount!;
}
