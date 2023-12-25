namespace EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurAssetsInvestedByUserIdCommand : IRequest<int>
{
    public UpdateEntrepreneurAssetsInvestedByUserIdCommand(UpdateEntrepreneurAssetsInvestedByUserIdCommand command)
    {
        UserId = command.UserId;
        AssetsInvested = command.AssetsInvested;
    }

    public long UserId { get; set; }
    public string AssetsInvested { get; set; } = string.Empty;
}

public class UpdateEntrepreneurAssetsInvestedByUserIdCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<UpdateEntrepreneurAssetsInvestedByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurAssetsInvestedByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.UserId} | Update Entrepreneur AssetsInvested");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}