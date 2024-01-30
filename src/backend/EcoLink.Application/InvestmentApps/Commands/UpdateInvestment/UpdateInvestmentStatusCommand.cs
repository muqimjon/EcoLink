namespace EcoLink.Application.InvestmentApps.Commands.UpdateInvestment;

public record UpdateInvestmentStatusCommand : IRequest<InvestmentAppResultDto>
{
    public UpdateInvestmentStatusCommand(UpdateInvestmentStatusCommand command)
    {
        UserId = command.UserId;
    }

    public long UserId { get; set; }
    public bool IsOld { get; set; } = true;
}

public class UpdateInvestmentStatusCommandHandler(IMapper mapper, IRepository<InvestmentApp> repository) :
    IRequestHandler<UpdateInvestmentStatusCommand, InvestmentAppResultDto>
{
    public async Task<InvestmentAppResultDto> Handle(UpdateInvestmentStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(e => e.UserId == request.UserId) ??
            throw new NotFoundException($"{nameof(InvestmentApp)} is not found with {nameof(request.UserId)} = {request.UserId}");

        mapper.Map(request, entity);
        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<InvestmentAppResultDto>(entity);
    }
}
