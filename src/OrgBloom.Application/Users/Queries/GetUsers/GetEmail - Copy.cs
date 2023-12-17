using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetDateOfBirthQuery : IRequest<DateTimeOffset>
{
    public GetDateOfBirthQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetDateOfBirthQueryHendler(IRepository<User> repository) : IRequestHandler<GetDateOfBirthQuery, DateTimeOffset>
{
    public async Task<DateTimeOffset> Handle(GetDateOfBirthQuery request, CancellationToken cancellationToken)
    {
        var d = await repository.SelectAsync(i => i.Id.Equals(request.Id));
        var o = d.DateOfBirth;
        return o ?? DateTimeOffset.MinValue;
    }
        //=> (await repository.SelectAsync(i => i.Id.Equals(request.Id))).DateOfBirth ?? DateTimeOffset.MinValue;
}
