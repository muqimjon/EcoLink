using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetLanguageCodeByTelegramIdQuery : IRequest<string>
{
    public GetLanguageCodeByTelegramIdQuery(long telegramId) { TelegramId = telegramId; }
    public long TelegramId { get; set; }
}

public class GetLanguageCodeByTelegramIdQueryHendler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetLanguageCodeByTelegramIdQuery, string>
{
    public async Task<string> Handle(GetLanguageCodeByTelegramIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.TelegramId.Equals(request.TelegramId))).LanguageCode
        ?? throw new NotFoundException($"User is not found with Telegram id = {request.TelegramId}");
}
