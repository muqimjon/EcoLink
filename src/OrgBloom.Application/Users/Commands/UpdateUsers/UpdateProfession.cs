using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Users;
using OrgBloom.Application.Commons.Helpers;

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
    public State State { get; set; } = State.WaitingForSelectProfession;
}

public class UpdateProfessionCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateProfessionCommand, int>
{
    public async Task<int> Handle(UpdateProfessionCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update profession");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}