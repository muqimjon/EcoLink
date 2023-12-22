namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateStateCommand : IRequest<int>
{
    public UpdateStateCommand(long id, State state)
    {
        Id = id;
        State = state;
    }

    public long Id { get; set; }
    public State State { get; set; }
}

public class UpdateStateCommandHandler(IRepository<User> repository, IMapper mapper) : 
    IRequestHandler<UpdateStateCommand, int>
{
    public async Task<int> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | state update");

        mapper.Map(request, entity);

        if (request.State == State.WaitingForSelectProfession)
            entity.Profession = UserProfession.None;

        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}