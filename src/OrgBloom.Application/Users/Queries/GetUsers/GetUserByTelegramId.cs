using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetUserByTelegramIdQuery : IRequest<UserTelegramResultDto>
{
    public GetUserByTelegramIdQuery(long telegramId) { TelegramId = telegramId; }
    public long TelegramId { get; set; }
}

public class GetUserByTelegramIdQueryHendler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserByTelegramIdQuery, UserTelegramResultDto>
{
    public async Task<UserTelegramResultDto> Handle(GetUserByTelegramIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserTelegramResultDto>(await repository.SelectAsync(i => i.TelegramId.Equals(request.TelegramId)))
        ?? throw new NotFoundException($"User is not found with Telegram id = {request.TelegramId}");
}
