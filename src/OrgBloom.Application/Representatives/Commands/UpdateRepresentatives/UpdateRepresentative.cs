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

    public long Id { get; set; }
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