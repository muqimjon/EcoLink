using MediatR;
using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Commands.Entrepreneurs.CreateEntrepreneurs;

public record class CreateEntrepreneurCommand : IRequest<int>
{
    public CreateEntrepreneurCommand(CreateEntrepreneurCommand command)
    {
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
        Project = command.Project;
        LastName = command.LastName;
        HelpType = command.HelpType;
        FirstName = command.FirstName;
        CreatedAt = command.CreatedAt;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        Experience = command.Experience;
        DateOfBirth = command.DateOfBirth;
        AssetsInvested = command.AssetsInvested;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public int TelegramId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string AssetsInvested { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
}

public class CreateEntrepreneurCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<CreateEntrepreneurCommand, int>
{
    public async Task<int> Handle(CreateEntrepreneurCommand request, CancellationToken cancellationToken)
    {
        await repository.InsertAsync(mapper.Map<Entrepreneur>(request));
        return await repository.SaveAsync();
    }
}