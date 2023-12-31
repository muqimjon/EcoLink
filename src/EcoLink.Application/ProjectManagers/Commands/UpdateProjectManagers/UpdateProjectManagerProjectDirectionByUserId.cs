﻿namespace EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerProjectDirectionByUserIdCommand : IRequest<int>
{
    public UpdateProjectManagerProjectDirectionByUserIdCommand(UpdateProjectManagerProjectDirectionByUserIdCommand command)
    {
        UserId = command.UserId;
        ProjectDirection = command.ProjectDirection;
    }

    public long UserId { get; set; }
    public string ProjectDirection { get; set; } = string.Empty;
}

public class UpdateProjectManagerAreaByUserIdCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : 
    IRequestHandler<UpdateProjectManagerProjectDirectionByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerProjectDirectionByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"PM is not found with id: {request.UserId} | update PM Area");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}