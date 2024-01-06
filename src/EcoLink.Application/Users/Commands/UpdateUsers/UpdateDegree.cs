namespace EcoLink.Application.Users.Commands.UpdateUsers;

public record UpdateDegreeCommand : IRequest<int>
{
    public UpdateDegreeCommand(UpdateDegreeCommand command)
    {
        Id = command.Id;
        Degree = command.Degree;
    }

    public long Id { get; set; }
    public string Degree { get; set; } = string.Empty;
}

public class UpdateDegreeCommandHandler(IRepository<User> repository, IMapper mapper) : 
    IRequestHandler<UpdateDegreeCommand, int>
{
    public async Task<int> Handle(UpdateDegreeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update degree");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}