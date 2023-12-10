using MediatR;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Commands.Investors.DeleteInvestors;

public record DeleteInvestorCommand : IRequest<bool>
{
    public int TelegramId { get; set; }
}

public class DeleteInvestorHandler : IRequestHandler<DeleteInvestorCommand, bool>
{
    private readonly IRepository<Investor> repository;
    public async Task<bool> Handle(DeleteInvestorCommand request, CancellationToken cancellationToken)
    {
        repository.Delete(x => x.TelegramId == request.TelegramId);
        return await repository.SaveAsync() > 0;
    }
}