namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>>
{
    public GetAllUsersQuery(GetAllUsersQuery command) { Id = command.Id; }
    public long Id { get; set; }
}

public class GetAllUsersQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<UserResultDto>>(await Task.Run(() => repository.SelectAll()));
}
