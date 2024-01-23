namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetUserByIdQuery : IRequest<UserResultDto>
{
    public GetUserByIdQuery(GetUserByIdQuery command) { Id = command.Id; }
    public long Id { get; set; }
}

public class GetUserByIdQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
        ?? throw new NotFoundException($"User is not found with ID = {request.Id}");
}
