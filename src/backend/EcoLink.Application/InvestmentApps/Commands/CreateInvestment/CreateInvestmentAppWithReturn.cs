namespace EcoLink.Application.InvestmentApps.Commands.CreateInvestment;

public record CreateInvestmentWithReturnCommand : IRequest<InvestmentAppResultDto>
{
    public CreateInvestmentWithReturnCommand(CreateInvestmentWithReturnCommand command)
    {
        Age = command.Age;
        UserId = command.UserId;
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
        Sector = command.Sector;
        LastName = command.LastName;
        FirstName = command.FirstName;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOld { get; set; }
    public long UserId { get; set; }
}

public class CreateInvestmentAppWithReturnCommandHandler(IMapper mapper, 
    IRepository<InvestmentApp> repository,
    ISheetsRepository<InvestmentAppForSheetsDto> sheetsRepository) : 
    IRequestHandler<CreateInvestmentWithReturnCommand, InvestmentAppResultDto>
{
    public async Task<InvestmentAppResultDto> Handle(CreateInvestmentWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<InvestmentApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        var SheetsDto = mapper.Map<InvestmentAppForSheetsDto>(entity);
        SheetsDto.WasCreated = entity.CreatedAt.ToString("dd.MM.yyyy");
        await sheetsRepository.InsertAsync(SheetsDto);

        return mapper.Map<InvestmentAppResultDto>(entity);
    }
}