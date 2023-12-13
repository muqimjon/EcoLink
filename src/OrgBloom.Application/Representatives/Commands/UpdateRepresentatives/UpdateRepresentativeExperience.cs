using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeExperienceCommand : IRequest<int>
{
    public UpdateRepresentativeExperienceCommand(UpdateRepresentativeExperienceCommand command)
    {
        Id = command.Id;
        Experience = command.Experience;
    }

    public long Id { get; set; }
    public string Experience { get; set; } = string.Empty;
}

public class UpdateRepresentativeExperienceCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeExperienceCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative Experience");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}