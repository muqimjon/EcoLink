using OrgBloom.Domain.Enums;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetProfessionQuery : IRequest<UserProfession>
{
    public GetProfessionQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetProfessionQueryHendler(IRepository<User> repository) : IRequestHandler<GetProfessionQuery, UserProfession>
{
    public async Task<UserProfession> Handle(GetProfessionQuery request, CancellationToken cancellationToken)
        => ((await repository.SelectAsync(i => i.Id.Equals(request.Id))) ?? new()).Profession;
}
