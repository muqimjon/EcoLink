namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetLanguageCodeQuery : IRequest<string>
{
    public GetLanguageCodeQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetLanguageCodeQueryHendler(IRepository<User> repository) : IRequestHandler<GetLanguageCodeQuery, string>
{
    public async Task<string> Handle(GetLanguageCodeQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.Id.Equals(request.Id))).LanguageCode
        ?? throw new NotFoundException($"User is not found with Telegram id = {request.Id}");
}
