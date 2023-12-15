using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurIsSubmittedCommand : IRequest<int>
{
    public UpdateEntrepreneurIsSubmittedCommand(UpdateEntrepreneurIsSubmittedCommand command)
    {
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateEntrepreneurIsSubmittedCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurIsSubmittedCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurIsSubmittedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.UserId} | Update Entrepreneur IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}