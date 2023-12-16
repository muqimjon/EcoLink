using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Representation;

namespace OrgBloom.Application.Representatives.Queries.GetRepresentatives;

public record GetRepresentativePurposeByUserIdQuery : IRequest<string>
{
    public GetRepresentativePurposeByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetRepresentativePurposeByUserIdQueryHendler(IRepository<Representative> repository) : IRequestHandler<GetRepresentativePurposeByUserIdQuery, string>
{
    public async Task<string> Handle(GetRepresentativePurposeByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).Purpose!;
}
