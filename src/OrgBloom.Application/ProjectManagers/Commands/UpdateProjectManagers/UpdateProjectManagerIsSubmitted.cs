using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerIsSubmittedCommand : IRequest<int>
{
    public UpdateProjectManagerIsSubmittedCommand(UpdateProjectManagerCommand command)
    {
        Id = command.Id;
        IsSubmitted = command.IsSubmitted;
    }

    public long Id { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateProjectManagerIsSubmittedCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerIsSubmittedCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerIsSubmittedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}