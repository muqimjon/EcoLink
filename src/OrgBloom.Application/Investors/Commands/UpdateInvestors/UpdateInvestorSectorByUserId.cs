namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorSectorByUserIdCommand : IRequest<int>
{
    public UpdateInvestorSectorByUserIdCommand(UpdateInvestorSectorByUserIdCommand command)
    {
        UserId = command.UserId;
        Sector = command.Sector;
    }

    public long UserId { get; set; }
    public string Sector { get; set; } = string.Empty;
}

public class UpdateInvestorSectorByUserIdCommandHandler(IRepository<Investor> repository, IMapper mapper) : 
    IRequestHandler<UpdateInvestorSectorByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateInvestorSectorByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.UserId} | Update Investor Sector");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}