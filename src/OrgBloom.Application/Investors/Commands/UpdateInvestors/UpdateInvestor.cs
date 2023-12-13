using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorCommand : IRequest<int>
{
    public UpdateInvestorCommand(UpdateInvestorCommand command)
    {
        Id = command.Id;
        UserId = command.UserId;
        Sector = command.Sector;
        InvestmentAmount = command.InvestmentAmount;
    }

    public long Id { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class UpdateInvestorCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorCommand, int>
{
    public async Task<int> Handle(UpdateInvestorCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.Id} by User UserId: {request.UserId} | Update Investor");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}