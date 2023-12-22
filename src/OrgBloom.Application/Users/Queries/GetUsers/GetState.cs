namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetStateQuery : IRequest<State>
{
    public GetStateQuery(long Id) { this.Id = Id; }
    public long Id { get; set; }
}

public class GetStateQueryHendler(IRepository<User> repository) : IRequestHandler<GetStateQuery, State>
{
    public async Task<State> Handle(GetStateQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.Id.Equals(request.Id))).State;
}
