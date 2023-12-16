using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateLanguagesCommand : IRequest<int>
{
    public UpdateLanguagesCommand(UpdateLanguagesCommand command)
    {
        Id = command.Id;    
        Languages = command.Languages;
    }

    public long Id { get; set; }
    public string Languages { get; set; } = string.Empty;
}

public class UpdateLanguagesCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateLanguagesCommand, int>
{
    public async Task<int> Handle(UpdateLanguagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | Languages update");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}