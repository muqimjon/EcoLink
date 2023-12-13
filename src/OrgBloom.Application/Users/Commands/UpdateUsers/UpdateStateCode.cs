using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Enums;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateStateCommand : IRequest<int>
{
    public UpdateStateCommand(long id, UserState state)
    {
        Id = id;    
        State = state;
    }

    public long Id { get; set; }
    public UserState State { get; set; }
}

public class UpdateUserStateCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateStateCommand, int>
{
    public async Task<int> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}