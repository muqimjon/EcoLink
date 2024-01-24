using EcoLink.Application.Representatives.DTOs;

namespace EcoLink.Application.Representatives.Commands.CreateRepresentatives;

public record CreateRepresentativeWithReturnCommand : IRequest<RepresentativeResultDto>
{
    public CreateRepresentativeWithReturnCommand(CreateRepresentativeWithReturnCommand command)
    {
        Area = command.Area;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Expectation = command.Expectation;
        IsSubmitted = command.IsSubmitted;
    }

    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class CreateRepresentativeWithReturnCommandHandler(IRepository<Representative> repository, IMapper mapper) : 
    IRequestHandler<CreateRepresentativeWithReturnCommand, RepresentativeResultDto>
{
    public async Task<RepresentativeResultDto> Handle(CreateRepresentativeWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Representation is already exist create Representation by user id {request.UserId}");

        entity = mapper.Map<Representative>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<RepresentativeResultDto>(entity);
    }
}