using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativePurposeCommand : IRequest<int>
{
    public UpdateRepresentativePurposeCommand(UpdateRepresentativePurposeCommand command)
    {
        Id = command.Id;
        Purpose = command.Purpose;
    }

    public long Id { get; set; }
    public string Purpose { get; set; } = string.Empty;
}

public class UpdateRepresentativePurposeCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativePurposeCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativePurposeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative Purpose");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}