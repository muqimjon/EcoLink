using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetLanguagesQuery : IRequest<string>
{
    public GetLanguagesQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetLanguagesQueryHendler(IRepository<User> repository) : IRequestHandler<GetLanguagesQuery, string>
{
    public async Task<string> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        => ((await repository.SelectAsync(i => i.Id.Equals(request.Id))) ?? new()).Languages!;
}
