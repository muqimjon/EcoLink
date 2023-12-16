using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Representation;

namespace OrgBloom.Application.Representatives.Queries.GetRepresentatives;

public record GetRepresentativeExpectationByUserIdQuery : IRequest<string>
{
    public GetRepresentativeExpectationByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetRepresentativeExpectationByUserIdQueryHendler(IRepository<Representative> repository) : IRequestHandler<GetRepresentativeExpectationByUserIdQuery, string>
{
    public async Task<string> Handle(GetRepresentativeExpectationByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).Expectation!;
}
