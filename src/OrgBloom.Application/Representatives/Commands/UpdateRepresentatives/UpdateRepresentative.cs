using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeCommand : IRequest<int>
{
    public UpdateRepresentativeCommand(UpdateRepresentativeCommand command)
    {
        Id = command.Id;
        Area = command.Area;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Address = command.Address;
        Languages = command.Languages;
        Experience = command.Experience;
        Expectation = command.Expectation;
    }

    public long Id { get; set; }
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class UpdateRepresentativeCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}