using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

public record UpdateRepresentativeAddressCommand : IRequest<int>
{
    public UpdateRepresentativeAddressCommand(UpdateRepresentativeAddressCommand command)
    {
        Id = command.Id;
        Address = command.Address;
    }

    public long Id { get; set; }
    public string Address { get; set; } = string.Empty;
}

public class UpdateRepresentativeAddressCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<UpdateRepresentativeAddressCommand, int>
{
    public async Task<int> Handle(UpdateRepresentativeAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Representative is not found with id: {request.Id} | update representative Address");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}