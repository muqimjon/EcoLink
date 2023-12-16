using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record IsUserNewQuery : IRequest<bool>
{
    public IsUserNewQuery(long telegramId) { Id = telegramId; }
    public long Id { get; set; }
}

public class IsUserNewQueryHendler(IRepository<User> repository) : IRequestHandler<IsUserNewQuery, bool>
{
    public async Task<bool> Handle(IsUserNewQuery request, CancellationToken cancellationToken)
        => DateTime.UtcNow - (await repository.SelectAsync(i => i.Id.Equals(request.Id))).CreatedAt < TimeSpan.FromSeconds(5);
}
