using AutoMapper;
using MediatR;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

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
    private readonly IMapper mapper;

    public async Task<int> Handle(UpdateInvestorsCommands request, CancellationToken cancellationToken)
    {
        repository.Update(mapper.Map<Investor>(request));
        return await repository.SaveAsync();
    }
}