using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeIsSubmittedCommand : IRequest<int>
{
    public UpdateRepresentativeIsSubmittedCommand(UpdateRepresentativeIsSubmittedCommand command)
    {
        Id = command.Id;
        IsSubmitted = command.IsSubmitted;
    }

    public long Id { get; set; }
    public string IsSubmitted { get; set; } = string.Empty;
}

public class UpdateRepresentativeIsSubmittedCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeIsSubmittedCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeIsSubmittedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}