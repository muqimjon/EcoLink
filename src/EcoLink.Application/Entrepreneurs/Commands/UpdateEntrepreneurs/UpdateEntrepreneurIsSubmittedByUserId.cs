namespace EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurIsSubmittedByUserIdCommand : IRequest<int>
{
    public UpdateEntrepreneurIsSubmittedByUserIdCommand(UpdateEntrepreneurIsSubmittedByUserIdCommand command)
    {
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateEntrepreneurIsSubmittedByUserIdCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<UpdateEntrepreneurIsSubmittedByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurIsSubmittedByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.UserId} | Update Entrepreneur IsSubmitted");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}