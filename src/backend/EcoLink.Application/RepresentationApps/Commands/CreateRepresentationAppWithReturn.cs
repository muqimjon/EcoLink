namespace EcoLink.Application.RepresentationApps.Commands.CreateRepresentationApps;

public record CreateRepresentationAppWithReturnCommand : IRequest<RepresentationAppResultDto>
{
    public CreateRepresentationAppWithReturnCommand(CreateRepresentationAppWithReturnCommand command)
    {
        Age = command.Age;
        Area = command.Area;
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
        UserId = command.UserId;
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
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateRepresentationAppWithReturnCommandHandler(IMapper mapper,
    IRepository<RepresentationApp> repository,
    ISheetsRepository<RepresentationAppForSheetsDto> sheetsRepository) : 
    IRequestHandler<CreateRepresentationAppWithReturnCommand, RepresentationAppResultDto>
{
    public async Task<RepresentationAppResultDto> Handle(CreateRepresentationAppWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<RepresentationApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        var SheetsDto = mapper.Map<RepresentationAppForSheetsDto>(entity);
        SheetsDto.WasCreated = entity.CreatedAt.ToString("dd.MM.yyyy");
        await sheetsRepository.InsertAsync(SheetsDto);

        return mapper.Map<RepresentationAppResultDto>(entity);
    }
}