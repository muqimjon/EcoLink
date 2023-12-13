using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurAssetsInvestedCommand : IRequest<int>
{
    public UpdateEntrepreneurAssetsInvestedCommand(UpdateEntrepreneurAssetsInvestedCommand command)
    {
        Id = command.Id;
        AssetsInvested = command.AssetsInvested;
    }

    public long Id { get; set; }
    public string AssetsInvested { get; set; } = string.Empty;
}

public class UpdateEntrepreneurAssetsInvestedCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurAssetsInvestedCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurAssetsInvestedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur AssetsInvested");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}