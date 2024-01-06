using EcoLink.Application.EntrepreneurshipApps.DTOs;

namespace EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;

public record CreateEntrepreneurshipAppWithReturnCommand : IRequest<EntrepreneurshipAppResultDto>
{
    public CreateEntrepreneurshipAppWithReturnCommand(CreateEntrepreneurshipAppWithReturnCommand command)
    {
        Age = command.Age;
        Phone = command.Phone;
        Email = command.Email;
        UserId = command.UserId;
        Degree = command.Degree;
        Project = command.Project;
        LastName = command.LastName;
        HelpType = command.HelpType;
        FirstName = command.FirstName;
        Experience = command.Experience;
        AssetsInvested = command.AssetsInvested;
        RequiredFunding = command.RequiredFunding;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateEntrepreneurshipAppWithReturnCommandHandler(IMapper mapper,
    IRepository<EntrepreneurshipApp> repository, 
    ISheetsRepository<EntrepreneurshipAppForSheetsDto> sheetsRepository) : 
    IRequestHandler<CreateEntrepreneurshipAppWithReturnCommand, EntrepreneurshipAppResultDto>
{
    public async Task<EntrepreneurshipAppResultDto> Handle(CreateEntrepreneurshipAppWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<EntrepreneurshipApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        var SheetsDto = mapper.Map<EntrepreneurshipAppForSheetsDto>(entity);
        SheetsDto.WasCreated = entity.CreatedAt.ToString("dd.MM.yyyy");
        await sheetsRepository.InsertAsync(SheetsDto);

        return mapper.Map<EntrepreneurshipAppResultDto>(entity);
    }
}