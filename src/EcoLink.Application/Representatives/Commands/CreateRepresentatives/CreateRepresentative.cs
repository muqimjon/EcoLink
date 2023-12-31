﻿namespace EcoLink.Application.Representatives.Commands.CreateRepresentatives;

public record CreateRepresentativeCommand : IRequest<int>
{
    public CreateRepresentativeCommand(CreateRepresentativeCommand command)
    {
        Area = command.Area;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Address = command.Address;
        Languages = command.Languages;
        Experience = command.Experience;
        Expectation = command.Expectation;
        IsSubmitted = command.IsSubmitted;
    }

    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class CreateRepresentativeCommandHandler(IRepository<Representative> repository, IMapper mapper) : 
    IRequestHandler<CreateRepresentativeCommand, int>
{
    public async Task<int> Handle(CreateRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Languages is already exist with user id: {request.UserId} | update representative");

        entity = mapper.Map<Representative>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        return await repository.SaveAsync();
    }
}