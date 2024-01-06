namespace EcoLink.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeExpectationByUserIdCommand : IRequest<int>
{
    public UpdateRepresentativeExpectationByUserIdCommand(UpdateRepresentativeExpectationByUserIdCommand command)
    {
        UserId = command.UserId;
        Expectation = command.Expectation;
    }

    public long UserId { get; set; }
    public string Expectation { get; set; } = string.Empty;
}

public class UpdateRepresentativeExpectationByUserIdCommandHandler(IRepository<Representative> repository, IMapper mapper) : 
    IRequestHandler<UpdateRepresentativeExpectationByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeExpectationByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Representative is not found with id: {request.UserId} | update representative Expectation");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}