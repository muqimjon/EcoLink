using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Representation;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeIsSubmittedByUserCommand : IRequest<int>
{
    public UpdateRepresentativeIsSubmittedByUserCommand(UpdateRepresentativeIsSubmittedByUserCommand command)
    {
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateRepresentativeIsSubmittedCommandByUserHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeIsSubmittedByUserCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeIsSubmittedByUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Representative is not found with id: {request.UserId} | update representative IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}