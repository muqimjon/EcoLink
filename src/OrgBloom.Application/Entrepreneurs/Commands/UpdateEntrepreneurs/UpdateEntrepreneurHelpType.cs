using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurHelpTypeCommand : IRequest<int>
{
    public UpdateEntrepreneurHelpTypeCommand(UpdateEntrepreneurHelpTypeCommand command)
    {
        Id = command.Id;
        HelpType = command.HelpType;
    }

    public long Id { get; set; }
    public string HelpType { get; set; } = string.Empty;
}

public class UpdateEntrepreneurHelpTypeCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurHelpTypeCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurHelpTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur HelpType");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}