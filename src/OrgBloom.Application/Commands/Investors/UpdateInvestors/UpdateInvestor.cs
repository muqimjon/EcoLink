using AutoMapper;
using MediatR;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Commands.Investors.UpdateInvestors;

public record UpdateInvestorCommand : IRequest<int>
{
    public UpdateInvestorCommand(UpdateInvestorCommand command)
    {
        Id = command.Id;
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

    public long Id { get; set; }
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
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();
        
        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}