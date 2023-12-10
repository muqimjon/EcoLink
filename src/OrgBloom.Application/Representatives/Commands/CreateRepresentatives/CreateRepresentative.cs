using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Representatives.Commands.CreateRepresentatives;

public record CreateRepresentativeCommand : IRequest<int>
{
    public CreateRepresentativeCommand(CreateRepresentativeCommand command)
    {
        Area = command.Area;
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
        Purpose = command.Purpose;
        Address = command.Address;
        LastName = command.LastName;
        Languages = command.Languages;
        FirstName = command.FirstName;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        Experience = command.Experience;
        Expectation = command.Expectation;
        DateOfBirth = command.DateOfBirth;

    }

    public int TelegramId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
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