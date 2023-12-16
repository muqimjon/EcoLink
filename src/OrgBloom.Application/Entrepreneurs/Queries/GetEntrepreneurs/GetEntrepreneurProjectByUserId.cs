using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Entrepreneurship;

namespace OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetEntrepreneurProjectByUserIdQuery : IRequest<string>
{
    public GetEntrepreneurProjectByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetEntrepreneurProjectByUserIdQueryHendler(IRepository<Entrepreneur> repository) : IRequestHandler<GetEntrepreneurProjectByUserIdQuery, string>
{
    public async Task<string> Handle(GetEntrepreneurProjectByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).Project!;
}
