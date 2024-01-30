namespace EcoLink.Application.ProjectManagementApps.Commands.UpdateProjectManagement;

public record UpdateProjectManagementStatusCommand : IRequest<ProjectManagementAppResultDto>
{
    public UpdateProjectManagementStatusCommand(UpdateProjectManagementStatusCommand command)
    {
        UserId = command.UserId;
    }

    public long UserId { get; set; }
    public bool IsOld { get; set; } = true;
}

public class UpdateProjectManagementStatusCommandHandler(IMapper mapper, IRepository<ProjectManagementApp> repository) :
    IRequestHandler<UpdateProjectManagementStatusCommand, ProjectManagementAppResultDto>
{
    public async Task<ProjectManagementAppResultDto> Handle(UpdateProjectManagementStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(e => e.UserId == request.UserId) ??
            throw new NotFoundException($"{nameof(ProjectManagementApp)} is not found with {nameof(request.UserId)} = {request.UserId}");

        mapper.Map(request, entity);
        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<ProjectManagementAppResultDto>(entity);
    }
}
