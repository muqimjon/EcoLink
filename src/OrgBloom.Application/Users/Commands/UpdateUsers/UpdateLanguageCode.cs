using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateLanguageCodeCommand : IRequest<int>
{
    public UpdateLanguageCodeCommand(UpdateLanguageCodeCommand command)
    {
        Id = command.Id;
        LanguageCode = command.LanguageCode;
    }

    public long Id { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
}

public class UpdateUserLanguageCodeCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateLanguageCodeCommand, int>
{
    public async Task<int> Handle(UpdateLanguageCodeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}