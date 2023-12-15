using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerIsSubmittedCommand : IRequest<int>
{
    public UpdateProjectManagerIsSubmittedCommand(UpdateProjectManagerIsSubmittedCommand command)
    {
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateProjectManagerIsSubmittedCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerIsSubmittedCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerIsSubmittedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"PM is not found with id: {request.UserId} | update PM IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}