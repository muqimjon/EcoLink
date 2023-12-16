using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdatePhoneCommand : IRequest<int>
{
    public UpdatePhoneCommand(UpdatePhoneCommand command)
    {
        Id = command.Id;
        Phone = command.Phone;
    }

    public long Id { get; set; }
    public string Phone { get; set; } = string.Empty;
}

public class UpdatePhoneCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdatePhoneCommand, int>
{
    public async Task<int> Handle(UpdatePhoneCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update phone");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}