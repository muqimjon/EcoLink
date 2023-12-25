namespace EcoLink.Application.Users.Queries.GetUsers;

public record IsUserNewQuery : IRequest<bool>
{
    public IsUserNewQuery(long telegramId) { Id = telegramId; }
    public long Id { get; set; }
}

public class IsUserNewQueryHendler(IRepository<User> repository) : IRequestHandler<IsUserNewQuery, bool>
{
    public async Task<bool> Handle(IsUserNewQuery request, CancellationToken cancellationToken)
        => DateTime.UtcNow.AddHours(TimeConstants.UTC) - (await repository.SelectAsync(i => i.Id.Equals(request.Id))).CreatedAt < TimeSpan.FromSeconds(5);
}
