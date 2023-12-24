﻿using OrgBloom.Application.RepresentationApps.DTOs;

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
        SheetsDto.Age = TimeHelper.GetAge(entity.DateOfBirth);
        SheetsDto.WasCreated = TimeHelper.GetDate(entity.CreatedAt);
        await sheetsRepository.InsertAsync(SheetsDto);

        return mapper.Map<RepresentationAppResultDto>(entity);
    }
}