namespace EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurProjectByUserIdCommand : IRequest<int>
{
    public UpdateEntrepreneurProjectByUserIdCommand(UpdateEntrepreneurProjectByUserIdCommand command)
    {
        UserId = command.UserId;
        Project = command.Project;
    }

    public long UserId { get; set; }
    public string Project { get; set; } = string.Empty;
}

public class UpdateEntrepreneurProjectCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<UpdateEntrepreneurProjectByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurProjectByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.UserId} | Update Entrepreneur Project");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}