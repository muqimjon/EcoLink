namespace EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurCommand : IRequest<int>
{
    public UpdateEntrepreneurCommand(UpdateEntrepreneurCommand command)
    {
        Id = command.Id;
        Sector = command.Sector;
        UserId = command.UserId;
        Project = command.Project;
        HelpType = command.HelpType;
        IsSubmitted = command.IsSubmitted;
        AssetsInvested = command.AssetsInvested;
        InvestmentAmount = command.InvestmentAmount;
    }

    public long Id { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public decimal? InvestmentAmount { get; set; }
    public string AssetsInvested { get; set; } = string.Empty;
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateEntrepreneurCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<UpdateEntrepreneurCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}