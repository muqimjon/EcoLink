namespace EcoLink.Application.Users.Commands.UpdateUsers;

public record UpdateStateAndProfessionCommand : IRequest<int>
{
    public UpdateStateAndProfessionCommand(UpdateStateAndProfessionCommand command)
    {
        Id = command.Id;
        State = command.State;
        Profession = command.Profession;
    }

    public long Id { get; set; }
    public State State { get; set; }
    public UserProfession Profession { get; set; }
}

public class UpdateStateAndProfessionCommandHandler(IRepository<User> repository, IMapper mapper) : 
    IRequestHandler<UpdateStateAndProfessionCommand, int>
{
    public async Task<int> Handle(UpdateStateAndProfessionCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | State and Profession update");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}