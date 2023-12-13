using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurProjectCommand : IRequest<int>
{
    public UpdateEntrepreneurProjectCommand(UpdateEntrepreneurProjectCommand command)
    {
        Id = command.Id;
        Project = command.Project;
    }

    public long Id { get; set; }
    public string Project { get; set; } = string.Empty;
}

public class UpdateEntrepreneurProjectCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurProjectCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur Project");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}