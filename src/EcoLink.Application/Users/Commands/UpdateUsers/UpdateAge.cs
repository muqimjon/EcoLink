namespace EcoLink.Application.Users.Commands.UpdateUsers;

public record UpdateAgeCommand : IRequest<int>
{
    public UpdateAgeCommand(UpdateAgeCommand command)
    {
        Id = command.Id;
        Age = command.Age;
    }

    public long Id { get; set; }
    public string Age { get; set; } = string.Empty;
}

public class UpdateAgeCommandHandler(IRepository<User> repository, IMapper mapper) : 
    IRequestHandler<UpdateAgeCommand, int>
{
    public async Task<int> Handle(UpdateAgeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update Age");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}