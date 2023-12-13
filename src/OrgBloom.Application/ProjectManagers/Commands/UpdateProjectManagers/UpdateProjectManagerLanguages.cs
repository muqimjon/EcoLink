using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerLanguagesCommand : IRequest<int>
{
    public UpdateProjectManagerLanguagesCommand(UpdateProjectManagerCommand command)
    {
        Id = command.Id;
        Languages = command.Languages;
    }

    public long Id { get; set; }
    public string Languages { get; set; } = string.Empty;
}

public class UpdateProjectManagerLanguagesCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerLanguagesCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerLanguagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM Languages");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}