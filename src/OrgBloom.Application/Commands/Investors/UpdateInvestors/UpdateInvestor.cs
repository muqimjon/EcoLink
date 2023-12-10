using AutoMapper;
using MediatR;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Commands.Investors.UpdateInvestors;

public record UpdateInvestorCommand : IRequest<int>
{
    public UpdateInvestorCommand(UpdateInvestorCommand command)
    {
        TelegramId = command.TelegramId;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Patronomyc = command.Patronomyc;
        DateOfBirth = command.DateOfBirth;
        Degree = command.Degree;
        Sector = command.Sector;
        InvestmentAmount = command.InvestmentAmount;
        Phone = command.Phone;
        Email = command.Email;
    }

    public int TelegramId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UpdateInvestorCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorCommand, int>
{
    public async Task<int> Handle(UpdateInvestorCommand request, CancellationToken cancellationToken)
    {
        repository.Update(mapper.Map<Investor>(request));
        return await repository.SaveAsync();
    }
}