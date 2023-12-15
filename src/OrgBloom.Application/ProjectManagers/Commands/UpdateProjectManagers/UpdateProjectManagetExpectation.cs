using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerExpectationCommand : IRequest<int>
{
    public UpdateProjectManagerExpectationCommand(UpdateProjectManagerExpectationCommand command)
    {
        Id = command.Id;
        Expectation = command.Expectation;
    }

    public long Id { get; set; }
    public string Expectation { get; set; } = string.Empty;
}

public class UpdateProjectManagerExpectationCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerExpectationCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerExpectationCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM Expectation");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}