using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurRequiredFundingCommand : IRequest<int>
{
    public UpdateEntrepreneurRequiredFundingCommand(UpdateEntrepreneurRequiredFundingCommand command)
    {
        Id = command.Id;
        RequiredFunding = command.RequiredFunding;
    }

    public long Id { get; set; }
    public string RequiredFunding { get; set; } = string.Empty;
}

public class UpdateEntrepreneurRequiredFundingCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurRequiredFundingCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurRequiredFundingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur RequiredFunding");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}