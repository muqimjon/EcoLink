namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetLanguageCodeByIdQuery : IRequest<string>
{
    public GetLanguageCodeByIdQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetLanguageCodeByIdQueryHendler(IRepository<User> repository) : IRequestHandler<GetLanguageCodeByIdQuery, string>
{
    public async Task<string> Handle(GetLanguageCodeByIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.Id.Equals(request.Id))).LanguageCode
        ?? throw new NotFoundException($"User is not found with Telegram id = {request.Id}");
}
