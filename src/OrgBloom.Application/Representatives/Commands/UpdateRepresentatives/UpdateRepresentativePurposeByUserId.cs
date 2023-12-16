using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Representation;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativePurposeByUserIdCommand : IRequest<int>
{
    public UpdateRepresentativePurposeByUserIdCommand(UpdateRepresentativePurposeByUserIdCommand command)
    {
        UserId = command.UserId;
        Purpose = command.Purpose;
    }

    public long UserId { get; set; }
    public string Purpose { get; set; } = string.Empty;
}

public class UpdateRepresentativePurposeByUserIdCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativePurposeByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativePurposeByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Representative is not found with id: {request.UserId} | update representative Purpose");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}