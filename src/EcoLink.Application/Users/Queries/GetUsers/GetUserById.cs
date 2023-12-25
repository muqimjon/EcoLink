namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetUserByIdQuery : IRequest<UserApplyResultDto>
{
    public GetUserByIdQuery(GetUserByIdQuery command) { Id = command.Id; }
    public long Id { get; set; }
}

public class GetUserQueryHendler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserApplyResultDto>
{
    public async Task<UserApplyResultDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserApplyResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
        ?? throw new NotFoundException($"User is not found with ID = {request.Id}");
}
