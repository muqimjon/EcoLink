using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetEntrepreneurRequiredFundingByUserIdQuery : IRequest<string>
{
    public GetEntrepreneurRequiredFundingByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetEntrepreneurRequiredFundingByUserIdQueryHendler(IRepository<Entrepreneur> repository) : IRequestHandler<GetEntrepreneurRequiredFundingByUserIdQuery, string>
{
    public async Task<string> Handle(GetEntrepreneurRequiredFundingByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).RequiredFunding!;
}
