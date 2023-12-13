using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Enums;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateProfessionCommand : IRequest<int>
{
    public UpdateProfessionCommand(UpdateProfessionCommand command)
    {
        Id = command.Id;
        Profession = command.Profession;
    }

    public long Id { get; set; }
    public UserProfession Profession { get; set; }
    public UserState State { get; set; } = UserState.WaitingForSelectProfession;
}

public class UpdateProfessionCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateProfessionCommand, int>
{
    public async Task<int> Handle(UpdateProfessionCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new("User is not found user update profession");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}