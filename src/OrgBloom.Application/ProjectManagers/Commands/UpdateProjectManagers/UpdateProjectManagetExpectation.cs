using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.ProjectManagement;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerExpectationByUserIdCommand : IRequest<int>
{
    public UpdateProjectManagerExpectationByUserIdCommand(UpdateProjectManagerExpectationByUserIdCommand command)
    {
        UserId = command.UserId;
        Expectation = command.Expectation;
    }

    public long UserId { get; set; }
    public string Expectation { get; set; } = string.Empty;
}

public class UpdateProjectManagerExpectationByUserIdCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerExpectationByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerExpectationByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.UserId)
            ?? throw new NotFoundException($"PM is not found with id: {request.UserId} | update PM Expectation");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}