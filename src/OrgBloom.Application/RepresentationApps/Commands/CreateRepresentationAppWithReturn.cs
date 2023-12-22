using OrgBloom.Application.RepresentationApps.DTOs;

namespace OrgBloom.Application.RepresentationApps.Commands.CreateRepresentationApps;

public record CreateRepresentationAppWithReturnCommand : IRequest<RepresentationAppResultDto>
{
    public CreateRepresentationAppWithReturnCommand(CreateRepresentationAppWithReturnCommand command)
    {
        Area = command.Area;
        Degree = command.Degree;
        Purpose = command.Purpose;
        Address = command.Address;
        LastName = command.LastName;
        FirstName = command.FirstName;
        Languages = command.Languages;
        Experience = command.Experience;
        DateOfBirth = command.DateOfBirth;
        Expectation = command.Expectation;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
}

public class CreateRepresentationAppWithReturnCommandHandler(IRepository<RepresentationApp> repository, IMapper mapper) : 
    IRequestHandler<CreateRepresentationAppWithReturnCommand, RepresentationAppResultDto>
{
    public async Task<RepresentationAppResultDto> Handle(CreateRepresentationAppWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<RepresentationApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<RepresentationAppResultDto>(entity);
    }
}