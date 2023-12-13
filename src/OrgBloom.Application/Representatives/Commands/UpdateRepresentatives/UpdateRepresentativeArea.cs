using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeAreaCommand : IRequest<int>
{
    public UpdateRepresentativeAreaCommand(UpdateRepresentativeAreaCommand command)
    {
        Id = command.Id;
        Area = command.Area;
    }

    public long Id { get; set; }
    public string Area { get; set; } = string.Empty;
}

public class UpdateRepresentativeAreaCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeAreaCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeAreaCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative Area");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}