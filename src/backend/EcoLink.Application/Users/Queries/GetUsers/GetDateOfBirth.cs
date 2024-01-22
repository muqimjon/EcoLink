namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetDateOfBirthQuery : IRequest<DateTimeOffset>
{
    public GetDateOfBirthQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetDateOfBirthQueryHendler(IRepository<User> repository) : IRequestHandler<GetDateOfBirthQuery, DateTimeOffset>
{
    public async Task<DateTimeOffset> Handle(GetDateOfBirthQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.Id.Equals(request.Id))).DateOfBirth ?? DateTimeOffset.MinValue;
}
