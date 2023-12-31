﻿namespace EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;

public record class CreateEntrepreneurCommand : IRequest<int>
{
    public CreateEntrepreneurCommand(CreateEntrepreneurCommand command)
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

public class CreateEntrepreneurCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<CreateEntrepreneurCommand, int>
{
    public async Task<int> Handle(CreateEntrepreneurCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Entrepreneur is already exist with user id: {request.UserId} | Create Entrepreneur");

        entity = mapper.Map<Entrepreneur>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(mapper.Map<Entrepreneur>(entity));
        return await repository.SaveAsync();
    }
}