using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Commands.DeleteUsers;

public record DeleteUserCommand : IRequest<bool>
{
    public DeleteUserCommand(DeleteUserCommand command) { Id = command.Id; }
    public long Id { get; set; }
}

public class DeleteUserCommandHandler(IRepository<User> repository) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();

        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}