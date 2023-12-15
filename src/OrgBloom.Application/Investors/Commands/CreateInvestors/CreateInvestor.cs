using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Investors.Commands.CreateInvestors;

public record CreateInvestorCommand : IRequest<int>
{
    public CreateInvestorCommand(CreateInvestorCommand command)
    {
        UserId = command.UserId;
        Sector = command.Sector;
        IsSubmitted = command.IsSubmitted;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string Sector { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class CreateInvestorCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<CreateInvestorCommand, int>
{
    public async Task<int> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            return 0; //throw new AlreadyExistException($"Investor is already exist create investor by user id {request.UserId}");

        await repository.InsertAsync(mapper.Map<Investor>(request));
        return await repository.SaveAsync();
    }
}