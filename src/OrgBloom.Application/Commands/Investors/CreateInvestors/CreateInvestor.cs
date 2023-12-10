using AutoMapper;
using MediatR;
using OrgBloom.Application.Interfaces;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Commands.Investors.CreateInvestors;

public record CreateInvestorCommand : IRequest<int>
{
    public CreateInvestorCommand(CreateInvestorCommand command)
    {
        Email = command.Email;
        Phone = command.Phone;
        Sector = command.Sector;
        Degree = command.Degree;
        LastName = command.LastName;
        FirstName = command.FirstName;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        DateOfBirth = command.DateOfBirth;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public int TelegramId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public decimal InvestmentAmount { get; set; }
}

public class CreateInvestorCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<CreateInvestorCommand, int>
{
    public async Task<int> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
    {
        await repository.InsertAsync(mapper.Map<Investor>(request));
        return await repository.SaveAsync();
    }
}