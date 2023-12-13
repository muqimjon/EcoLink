using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetUserByTelegramIdQuery : IRequest<UserTelegramResultDto>
{
    public GetUserByTelegramIdQuery(long telegramId) { TelegramId = telegramId; }
    public long TelegramId { get; set; }
}

public class GetUserByTelegramIdQueryHendler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserByTelegramIdQuery, UserTelegramResultDto>
{
    public async Task<UserTelegramResultDto> Handle(GetUserByTelegramIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserTelegramResultDto>(await repository.SelectAsync(i => i.TelegramId == request.TelegramId));
}
