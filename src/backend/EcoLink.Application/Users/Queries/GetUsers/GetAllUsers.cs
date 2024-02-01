namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>>
{
}

public class GetAllUsersQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var d = (await Task.Run(() => repository.SelectAll(includes: ["Application"]))).ToList();
        var s = mapper.Map<IEnumerable<UserResultDto>>(d);

        return s;
    }
}
