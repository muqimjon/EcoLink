using MediatR;
using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;
using OrgBloom.Application.DTOs.Representatives;

namespace OrgBloom.Application.Commands.Representatives.DeleteRepresentatives;

public record DeleteRepresentativeCommand : IRequest<bool>
{
    public DeleteRepresentativeCommand(DeleteRepresentativeCommand command) { Id = command.Id; }
    public long Id { get; set; }
}

public class DeleteRepresentativeCommandHandler(IRepository<Representative> repository) : IRequestHandler<DeleteRepresentativeCommand, bool>
{
    public async Task<bool> Handle(DeleteRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();

        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}