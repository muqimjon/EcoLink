namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetUserByTelegramIdQuery : IRequest<UserResultDto>
{
    public GetUserByTelegramIdQuery(long telegramId) { TelegramId = telegramId; }
    public long TelegramId { get; set; }
}

public class GetUserByTelegramIdQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserByTelegramIdQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserByTelegramIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.TelegramId == request.TelegramId) 
            ?? throw new NotFoundException($"User is not found with Telegram ID={request.TelegramId}"));
}
