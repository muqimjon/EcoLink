﻿namespace EcoLink.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeAreaByUserIdCommand : IRequest<int>
{
    public UpdateRepresentativeAreaByUserIdCommand(UpdateRepresentativeAreaByUserIdCommand command)
    {
        UserId = command.UserId;
        Area = command.Area;
    }

    public long UserId { get; set; }
    public string Area { get; set; } = string.Empty;
}

public class UpdateRepresentativeAreaByUserIdCommandHandler(IRepository<Representative> repository, IMapper mapper) : 
    IRequestHandler<UpdateRepresentativeAreaByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeAreaByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Representative is not found with id: {request.UserId} | update representative Area");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}