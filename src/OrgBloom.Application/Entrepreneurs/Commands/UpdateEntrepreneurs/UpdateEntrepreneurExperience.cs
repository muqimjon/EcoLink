using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurExperienceCommand : IRequest<int>
{
    public UpdateEntrepreneurExperienceCommand(UpdateEntrepreneurExperienceCommand command)
    {
        Id = command.Id;
        Experience = command.Experience;
    }

    public long Id { get; set; }
    public string Experience { get; set; } = string.Empty;
}

public class UpdateEntrepreneurExperienceCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurExperienceCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur Experience");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}