using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorInvestmentAmountByUserIdCommand : IRequest<int>
{
    public UpdateInvestorInvestmentAmountByUserIdCommand(UpdateInvestorInvestmentAmountByUserIdCommand command)
    {
        UserId = command.UserId;
        InvestmentAmount = command.InvestmentAmount;
    }

    public long UserId { get; set; }
    public string InvestmentAmount { get; set; } = string.Empty;
}

public class UpdateInvestorInvestmentAmountByUserIdCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorInvestmentAmountByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateInvestorInvestmentAmountByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.UserId} | Update Investor OutvestmentAmount");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}