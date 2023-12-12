using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record IsUserExistByTelegramIdQuery : IRequest<bool>
{
    public IsUserExistByTelegramIdQuery(long telegramId) { TelegramId = telegramId; }
    public long TelegramId { get; set; }
}

public class IsUserExistByTelegramIdQueryHendler(IRepository<User> repository) : IRequestHandler<IsUserExistByTelegramIdQuery, bool>
{
    public async Task<bool> Handle(IsUserExistByTelegramIdQuery request, CancellationToken cancellationToken)
        => await repository.SelectAsync(i => i.TelegramId.Equals(request.TelegramId)) is not null;
}
