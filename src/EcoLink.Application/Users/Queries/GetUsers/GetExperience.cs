namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetExperienceQuery : IRequest<string>
{
    public GetExperienceQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetExperienceQueryHendler(IRepository<User> repository) : IRequestHandler<GetExperienceQuery, string>
{
    public async Task<string> Handle(GetExperienceQuery request, CancellationToken cancellationToken)
        => ((await repository.SelectAsync(i => i.Id.Equals(request.Id))) ?? new()).Experience!;
}
