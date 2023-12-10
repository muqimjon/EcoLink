using MediatR;
using OrgBloom.Application.Interfaces;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Queries.GetInvestors;

public record GetInvestorCommand : IRequest<Investor>
{
    public int TelegramId { get; set; }
}

public class GetInvestorCommandHendler : IRequestHandler<GetInvestorCommand, Investor>
{
    private readonly IRepository<Investor> repository;

    public async Task<Investor> Handle(GetInvestorCommand request, CancellationToken cancellationToken)
    {
        var id = request.TelegramId;
        return await repository.SelectAsync(i => i.TelegramId.Equals(request.TelegramId));
    }
}
