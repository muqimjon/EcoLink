using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeExpectationCommand : IRequest<int>
{
    public UpdateRepresentativeExpectationCommand(UpdateRepresentativeExpectationCommand command)
    {
        Id = command.Id;
        Expectation = command.Expectation;
    }

    public long Id { get; set; }
    public string Expectation { get; set; } = string.Empty;
}

public class UpdateRepresentativeExpectationCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeExpectationCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeExpectationCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative Expectation");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}