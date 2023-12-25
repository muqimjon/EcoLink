namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetEmailQuery : IRequest<string>
{
    public GetEmailQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetEmailQueryHendler(IRepository<User> repository) : IRequestHandler<GetEmailQuery, string>
{
    public async Task<string> Handle(GetEmailQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.Id.Equals(request.Id))).Email
        ?? throw new NotFoundException($"User is not found with id = {request.Id}");
}
