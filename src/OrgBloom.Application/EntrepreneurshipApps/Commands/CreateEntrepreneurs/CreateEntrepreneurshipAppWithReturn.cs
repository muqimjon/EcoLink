using AutoMapper;
using OrgBloom.Application.Commons.Helpers;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Entrepreneurship;
using OrgBloom.Application.EntrepreneurshipApps.DTOs;

namespace OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;

public record CreateEntrepreneurshipAppWithReturnCommand : IRequest<EntrepreneurshipAppResultDto>
{
    public CreateEntrepreneurshipAppWithReturnCommand(CreateEntrepreneurshipAppWithReturnCommand command)
    {
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
        Project = command.Project;
        LastName = command.LastName;
        HelpType = command.HelpType;
        FirstName = command.FirstName;
        Experience = command.Experience;
        DateOfBirth = command.DateOfBirth;
        AssetsInvested = command.AssetsInvested;
        RequiredFunding = command.RequiredFunding;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CreateEntrepreneurshipAppWithReturnCommandHandler(IRepository<EntrepreneurshipApp> repository, IMapper mapper) : IRequestHandler<CreateEntrepreneurshipAppWithReturnCommand, EntrepreneurshipAppResultDto>
{
    public async Task<EntrepreneurshipAppResultDto> Handle(CreateEntrepreneurshipAppWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<EntrepreneurshipApp>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<EntrepreneurshipAppResultDto>(entity);
    }
}