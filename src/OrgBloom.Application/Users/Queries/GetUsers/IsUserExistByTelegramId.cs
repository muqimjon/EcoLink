using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record IsUserNewQuery : IRequest<bool>
{
    public IsUserNewQuery(long telegramId) { Id = telegramId; }
    public long Id { get; set; }
}

public class IsUserNewQueryHendler(IRepository<User> repository) : IRequestHandler<IsUserNewQuery, bool>
{
    public async Task<bool> Handle(IsUserNewQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.SelectAsync(i => i.Id.Equals(request.Id));

        return DateTime.UtcNow - user.CreatedAt < TimeSpan.FromSeconds(5);
    }
}
