namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetAgeQuery : IRequest<string>
{
    public GetAgeQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetAgeQueryHendler(IRepository<User> repository) : IRequestHandler<GetAgeQuery, string>
{
    public async Task<string> Handle(GetAgeQuery request, CancellationToken cancellationToken)
        => ((await repository.SelectAsync(i => i.Id.Equals(request.Id))) ?? new()).Age!;
}
