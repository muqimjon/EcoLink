using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorIsSubmittedCommand : IRequest<int>
{
    public UpdateInvestorIsSubmittedCommand(UpdateInvestorIsSubmittedCommand command)
    {
        Id = command.Id;
        IsSubmitted = command.IsSubmitted;
    }

    public long Id { get; set; }
    public string IsSubmitted { get; set; } = string.Empty;
}

public class UpdateInvestorIsSubmittedCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorIsSubmittedCommand, int>
{
    public async Task<int> Handle(UpdateInvestorIsSubmittedCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.Id} | Update Investor IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}