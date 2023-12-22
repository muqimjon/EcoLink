namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetAddressQuery : IRequest<string>
{
    public GetAddressQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetAddressQueryHendler(IRepository<User> repository) : IRequestHandler<GetAddressQuery, string>
{
    public async Task<string> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        => ((await repository.SelectAsync(i => i.Id.Equals(request.Id))) ?? new()).Address!;
}
