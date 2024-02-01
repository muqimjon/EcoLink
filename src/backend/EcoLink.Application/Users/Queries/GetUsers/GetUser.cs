namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetUserQuery : IRequest<UserResultDto>
{
    public GetUserQuery(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class GetUserQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id), includes: ["Application"]))
        ?? throw new NotFoundException($"User is not found with ID = {request.Id}");
}
