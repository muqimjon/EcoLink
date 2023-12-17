using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Users;
using OrgBloom.Application.Commons.Helpers;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateExperienceCommand : IRequest<int>
{
    public UpdateExperienceCommand(UpdateExperienceCommand command)
    {
        Id = command.Id;    
        Experience = command.Experience;
    }

    public long Id { get; set; }
    public string Experience { get; set; } = string.Empty;
}

public class UpdateExperienceCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateExperienceCommand, int>
{
    public async Task<int> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | Experience update");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}