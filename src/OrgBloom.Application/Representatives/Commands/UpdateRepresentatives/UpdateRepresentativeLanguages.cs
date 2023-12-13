using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeLanguagesCommand : IRequest<int>
{
    public UpdateRepresentativeLanguagesCommand(UpdateRepresentativeLanguagesCommand command)
    {
        Id = command.Id;
        Languages = command.Languages;
    }

    public long Id { get; set; }
    public string Languages { get; set; } = string.Empty;
}

public class UpdateRepresentativeLanguagesCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeLanguagesCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeLanguagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative languages");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}