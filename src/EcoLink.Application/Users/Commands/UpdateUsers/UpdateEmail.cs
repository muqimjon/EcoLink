﻿namespace EcoLink.Application.Users.Commands.UpdateUsers;

public record UpdateEmailCommand : IRequest<int>
{
    public UpdateEmailCommand(UpdateEmailCommand command)
    {
        Id = command.Id;
        Email = command.Email;
    }

    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
}

public class UpdateEmailCommandHandler(IRepository<User> repository, IMapper mapper) : 
    IRequestHandler<UpdateEmailCommand, int>
{
    public async Task<int> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update email");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}