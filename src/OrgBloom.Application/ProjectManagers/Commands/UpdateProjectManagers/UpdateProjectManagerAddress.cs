using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

public record UpdateProjectManagerAddressCommand : IRequest<int>
{
    public UpdateProjectManagerAddressCommand(UpdateProjectManagerCommand command)
    {
        Id = command.Id;
        Address = command.Address;
    }

    public long Id { get; set; }
    public string Address { get; set; } = string.Empty;
}

public class UpdateProjectManagerAddressCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerAddressCommand, int>
{
    public async Task<int> Handle(UpdateProjectManagerAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"PM is not found with id: {request.Id} | update PM Address");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}