using OrgBloom.Domain.Enums;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetUserStateQuery : IRequest<UserState>
{
    public GetUserStateQuery(long telegramId) { Id = telegramId; }
    public long Id { get; set; }
}

public class GetUserStateQueryHendler(IRepository<User> repository) : IRequestHandler<GetUserStateQuery, UserState>
{
    public async Task<UserState> Handle(GetUserStateQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.Id.Equals(request.Id))).State;
}
