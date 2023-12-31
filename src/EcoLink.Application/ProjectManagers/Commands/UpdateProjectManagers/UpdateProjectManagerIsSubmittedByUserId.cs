﻿namespace EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerIsSubmittedByUserIdCommand : IRequest<int>
{
    public UpdateProjectManagerIsSubmittedByUserIdCommand(UpdateProjectManagerIsSubmittedByUserIdCommand command)
    {
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateProjectManagerIsSubmittedByUserIdCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : 
    IRequestHandler<UpdateProjectManagerIsSubmittedByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerIsSubmittedByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"PM is not found with id: {request.UserId} | update PM IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}