using MediatR;
using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Commands.ProjectManagers.DeleteProjectManagers;

public record DeleteProjectManagerCommand : IRequest<bool>
{
    public DeleteProjectManagerCommand(DeleteProjectManagerCommand command) { Id = command.Id; }
    public long Id { get; set; }
}

public class DeleteProjectManagerCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<DeleteProjectManagerCommand, bool>
{
    public Task<bool> Handle(DeleteProjectManagerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}