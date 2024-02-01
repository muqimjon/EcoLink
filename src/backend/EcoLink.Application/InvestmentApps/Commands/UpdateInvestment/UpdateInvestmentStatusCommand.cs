namespace EcoLink.Application.InvestmentApps.Commands.UpdateInvestment;

public record UpdateInvestmentStatusCommand : IRequest<InvestmentAppResultDto>
{
    public UpdateInvestmentStatusCommand(UpdateInvestmentStatusCommand command)
    {
        UserId = command.UserId;
        IsOld = command.IsOld;
    }

    public long UserId { get; set; }
    public bool IsOld { get; set; } = true;
}

public class UpdateInvestmentStatusCommandHandler(IMapper mapper, IRepository<InvestmentApp> repository) :
    IRequestHandler<UpdateInvestmentStatusCommand, InvestmentAppResultDto>
{
    public async Task<InvestmentAppResultDto> Handle(UpdateInvestmentStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = repository.SelectAll(e => e.UserId == request.UserId).OrderBy(e => e.Id).LastOrDefault() ??
            throw new NotFoundException($"{nameof(InvestmentApp)} is not found with {nameof(request.UserId)} = {request.UserId}");

        mapper.Map(request, entity);
        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<InvestmentAppResultDto>(entity);
    }
}
