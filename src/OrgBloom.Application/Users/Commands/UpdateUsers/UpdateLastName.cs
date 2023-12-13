using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateLastNameCommand : IRequest<int>
{
    public UpdateLastNameCommand(UpdateLastNameCommand command)
    {
        Id = command.Id;
        LastName = command.LastName;
    }

    public long Id { get; set; }
    public string LastName { get; set; } = string.Empty;
}

public class UpdateLastNameCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateLastNameCommand, int>
{
    public async Task<int> Handle(UpdateLastNameCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update lastname");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}