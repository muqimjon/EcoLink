using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Representatives.Commands.CreateRepresentatives;

public record CreateRepresentativeCommand : IRequest<int>
{
    public CreateRepresentativeCommand(CreateRepresentativeCommand command)
    {
        Area = command.Area;
        Purpose = command.Purpose;
        Address = command.Address;
        Languages = command.Languages;
        Experience = command.Experience;
        Expectation = command.Expectation;
        UserId = command.UserId;
    }

    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateRepresentativeCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<CreateRepresentativeCommand, int>
{
    public async Task<int> Handle(CreateRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.TelegramId == request.TelegramId);
        if (entity is not null)
            throw new();

        await repository.InsertAsync(mapper.Map<Representative>(request));
        return await repository.SaveAsync();
    }
}