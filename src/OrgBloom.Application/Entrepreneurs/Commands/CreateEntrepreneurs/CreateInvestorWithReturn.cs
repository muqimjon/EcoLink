using AutoMapper;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Entrepreneurship;
using OrgBloom.Application.Commons.Helpers;

namespace OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;

public record CreateEntrepreneurWithReturnCommand : IRequest<EntrepreneurResultDto>
{
    public CreateEntrepreneurWithReturnCommand(CreateEntrepreneurWithReturnCommand command)
    {
        Sector = command.Sector;
        UserId = command.UserId;
        Project = command.Project;
        HelpType = command.HelpType;
        AssetsInvested = command.AssetsInvested;
        RequiredFunding = command.RequiredFunding;
    }

    public string Sector { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateEntrepreneurWithReturnCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<CreateEntrepreneurWithReturnCommand, EntrepreneurResultDto>
{
    public async Task<EntrepreneurResultDto> Handle(CreateEntrepreneurWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Entrepreneur is already exist create Entrepreneur by user id {request.UserId}");

        entity = mapper.Map<Entrepreneur>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<EntrepreneurResultDto>(entity);
    }
}