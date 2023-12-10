using AutoMapper;
using MediatR;
using OrgBloom.Application.Interfaces;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Commands.Investors.CreateInvestors;

public record CreateInvestorCommand : IRequest<int>
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

public class CreateInvestorCommandHandler : IRequestHandler<CreateInvestorCommand, int>
{
    private readonly IRepository<Investor> repository;
    private readonly IMapper mappers;

    public async Task<int> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
    {
        await repository.InsertAsync(mappers.Map<Investor>(request));
        return await repository.SaveAsync();
    }
}