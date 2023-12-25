namespace EcoLink.Application.Users.Commands.UpdateUsers;

public record UpdateFirstNameCommand : IRequest<int>
{
    public UpdateFirstNameCommand(UpdateFirstNameCommand command)
    {
        Id = command.Id;
        FirstName = command.FirstName;
    }

    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
}

public class UpdateFirstNameCommandHandler(IRepository<User> repository, IMapper mapper) : 
    IRequestHandler<UpdateFirstNameCommand, int>
{
    public async Task<int> Handle(UpdateFirstNameCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update first name");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}