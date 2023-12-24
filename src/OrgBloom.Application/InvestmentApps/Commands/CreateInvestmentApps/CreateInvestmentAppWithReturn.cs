using OrgBloom.Application.InvestmentApps.DTOs;

namespace OrgBloom.Application.InvestmentApps.Commands.CreateInvestmentApps;

public record CreateInvestmentAppWithReturnCommand : IRequest<InvestmentAppResultDto>
{
    public CreateInvestmentAppWithReturnCommand(CreateInvestmentAppWithReturnCommand command)
    {
        UserId = command.UserId;
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
        Sector = command.Sector;
        LastName = command.LastName;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateInvestmentAppWithReturnCommandHandler(IMapper mapper, 
    IRepository<InvestmentApp> repository,
    ISheetsRepository<InvestmentAppResultDto> sheetsRepository) : 
    IRequestHandler<CreateInvestmentAppWithReturnCommand, InvestmentAppResultDto>
{
    public async Task<InvestmentAppResultDto> Handle(CreateInvestmentAppWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<InvestmentApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();
        var mapped = mapper.Map<InvestmentAppResultDto>(entity);
        await sheetsRepository.InsertAsync(mapped);

        return mapped;
    }
}