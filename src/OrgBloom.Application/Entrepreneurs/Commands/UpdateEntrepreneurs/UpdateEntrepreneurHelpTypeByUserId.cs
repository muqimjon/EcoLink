namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurHelpTypeByUserIdCommand : IRequest<int>
{
    public UpdateEntrepreneurHelpTypeByUserIdCommand(UpdateEntrepreneurHelpTypeByUserIdCommand command)
    {
        UserId = command.UserId;
        HelpType = command.HelpType;
    }

    public long UserId { get; set; }
    public string HelpType { get; set; } = string.Empty;
}

public class UpdateEntrepreneurHelpTypeByUserIdCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<UpdateEntrepreneurHelpTypeByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurHelpTypeByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.UserId} | Update Entrepreneur HelpType");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}