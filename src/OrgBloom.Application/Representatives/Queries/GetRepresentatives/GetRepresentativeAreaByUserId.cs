using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Representatives.Queries.GetRepresentatives;

public record GetRepresentativeAreaByUserIdQuery : IRequest<string>
{
    public GetRepresentativeAreaByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetRepresentativeAreaByUserIdQueryHendler(IRepository<Representative> repository) : IRequestHandler<GetRepresentativeAreaByUserIdQuery, string>
{
    public async Task<string> Handle(GetRepresentativeAreaByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).Area!;
}
