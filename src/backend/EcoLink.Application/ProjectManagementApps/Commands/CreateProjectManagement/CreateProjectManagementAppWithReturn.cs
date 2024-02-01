namespace EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagement;

public record CreateProjectManagementWithReturnCommand : IRequest<ProjectManagementAppResultDto>
{
    public CreateProjectManagementWithReturnCommand(CreateProjectManagementWithReturnCommand command)
    {
        Age = command.Age;
        Phone = command.Phone;
        Email = command.Email;
        UserId = command.UserId;
        Degree = command.Degree;
        Sector = command.Sector;
        Purpose = command.Purpose;
        Address = command.Address;
        LastName = command.LastName;
        FirstName = command.FirstName;
        Languages = command.Languages;
        Experience = command.Experience;
        Expectation = command.Expectation;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOld { get; set; }
    public long UserId { get; set; }
}

public class CreateProjectManagementAppWithReturnCommandHandler(IMapper mapper,
    IRepository<ProjectManagementApp> repository,
    ISheetsRepository<ProjectManagementAppForSheetsDto> sheetsRepository) :
    IRequestHandler<CreateProjectManagementWithReturnCommand, ProjectManagementAppResultDto>
{
    public async Task<ProjectManagementAppResultDto> Handle(
        CreateProjectManagementWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ProjectManagementApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        var SheetsDto = mapper.Map<ProjectManagementAppForSheetsDto>(entity);
        SheetsDto.WasCreated = entity.CreatedAt.ToString("dd.MM.yyyy");
        await sheetsRepository.InsertAsync(SheetsDto);

        return mapper.Map<ProjectManagementAppResultDto>(entity);
    }
}