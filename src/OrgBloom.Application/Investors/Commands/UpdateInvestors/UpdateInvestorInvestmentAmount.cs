using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorInvestmentAmountCommand : IRequest<int>
{
    public UpdateInvestorInvestmentAmountCommand(UpdateInvestorInvestmentAmountCommand command)
    {
        Id = command.Id;
        InvestmentAmount = command.InvestmentAmount;
    }

    public long Id { get; set; }
    public string InvestmentAmount { get; set; } = string.Empty;
}

public class UpdateInvestorInvestmentAmountCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorInvestmentAmountCommand, int>
{
    public async Task<int> Handle(UpdateInvestorInvestmentAmountCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.Id} | Update Investor InvestmentAmount");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}