using MediatR;
using OrgBloom.Application.Interfaces;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Commands.Investors.UpdateInvestors;

public record UpdateInvestorsCommands : IRequest<int>
{
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

public class UpdateInvestorsCommandHandler : IRequestHandler<UpdateInvestorsCommands, int>
{
    private readonly IRepository<Investor> repository;

    public Task<int> Handle(UpdateInvestorsCommands request, CancellationToken cancellationToken)
    {
        return repository.Update(
    }
}