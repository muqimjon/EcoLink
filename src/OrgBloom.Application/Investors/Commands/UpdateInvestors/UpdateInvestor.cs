using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorCommand : IRequest<int>
{
    public UpdateInvestorCommand(UpdateInvestorCommand command)
    {
        Id = command.Id;
        Email = command.Email;
        Phone = command.Phone;
        Sector = command.Sector;
        Degree = command.Degree;
        LastName = command.LastName;
        FirstName = command.FirstName;
        Patronomyc = command.Patronomyc;
        TelegramId = command.TelegramId;
        DateOfBirth = command.DateOfBirth;
        InvestmentAmount = command.InvestmentAmount;
    }

    public long Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public int TelegramId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public decimal InvestmentAmount { get; set; }
}

public class UpdateInvestorCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorCommand, int>
{
    public async Task<int> Handle(UpdateInvestorCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}