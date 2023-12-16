using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetEntrepreneurAssetsInvestedByUserIdQuery : IRequest<string>
{
    public GetEntrepreneurAssetsInvestedByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetEntrepreneurAssetsInvestedByUserIdQueryHendler(IRepository<Entrepreneur> repository) : IRequestHandler<GetEntrepreneurAssetsInvestedByUserIdQuery, string>
{
    public async Task<string> Handle(GetEntrepreneurAssetsInvestedByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).AssetsInvested!;
}
