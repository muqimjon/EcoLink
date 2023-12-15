using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorIsSubmittedCommand : IRequest<int>
{
    public UpdateInvestorIsSubmittedCommand(UpdateInvestorIsSubmittedCommand command)
    {
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateInvestorIsSubmittedCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorIsSubmittedCommand, int>
{
    public async Task<int> Handle(UpdateInvestorIsSubmittedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.UserId} | Update Investor IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}