using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurIsSubmittedCommand : IRequest<int>
{
    public UpdateEntrepreneurIsSubmittedCommand(UpdateEntrepreneurIsSubmittedCommand command)
    {
        Id = command.Id;
        IsSubmitted = command.IsSubmitted;
    }

    public long Id { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateEntrepreneurIsSubmittedCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurIsSubmittedCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurIsSubmittedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}