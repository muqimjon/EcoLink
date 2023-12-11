using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;

public record class CreateEntrepreneurCommand : IRequest<int>
{
    public CreateEntrepreneurCommand(CreateEntrepreneurCommand command)
    {
        UserId = command.UserId;
        Project = command.Project;
        HelpType = command.HelpType;
        Experience = command.Experience;
        AssetsInvested = command.AssetsInvested;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string Experience { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public decimal? InvestmentAmount { get; set; }
    public string AssetsInvested { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateEntrepreneurCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<CreateEntrepreneurCommand, int>
{
    public async Task<int> Handle(CreateEntrepreneurCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new();

        await repository.InsertAsync(mapper.Map<Entrepreneur>(request));
        return await repository.SaveAsync();
    }
}