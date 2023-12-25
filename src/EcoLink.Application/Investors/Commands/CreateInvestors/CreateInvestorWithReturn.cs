using EcoLink.Application.Investors.DTOs;

namespace EcoLink.Application.Investors.Commands.CreateInvestors;

public record CreateInvestorWithReturnCommand : IRequest<InvestorResultDto>
{
    public CreateInvestorWithReturnCommand(CreateInvestorWithReturnCommand command)
    {
        UserId = command.UserId;
        Sector = command.Sector;
        IsSubmitted = command.IsSubmitted;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class CreateInvestorWithReturnCommandHandler(IRepository<Investor> repository, IMapper mapper) : 
    IRequestHandler<CreateInvestorWithReturnCommand, InvestorResultDto>
{
    public async Task<InvestorResultDto> Handle(CreateInvestorWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Investor is already exist create investor by user id {request.UserId}");

        entity = mapper.Map<Investor>(request);
        entity.CreatedAt = TimeHelper.GetDateTime();
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<InvestorResultDto>(entity);
    }
}