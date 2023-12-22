namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurSectorByUserIdCommand : IRequest<int>
{
    public UpdateEntrepreneurSectorByUserIdCommand(UpdateEntrepreneurSectorByUserIdCommand command)
    {
        UserId = command.UserId;
        Sector = command.Sector;
    }

    public long UserId { get; set; }
    public string Sector { get; set; } = string.Empty;
}

public class UpdateEntrepreneurSectorByUserIdCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<UpdateEntrepreneurSectorByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurSectorByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.UserId} | Update Entrepreneur Sector");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}