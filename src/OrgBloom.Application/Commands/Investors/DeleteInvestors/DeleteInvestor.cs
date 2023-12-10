using MediatR;

namespace OrgBloom.Application.Commands.Investors.DeleteInvestors;

public record DeleteInvestorCommand : IRequest<bool>
{
    public int TelegramId { get; set; }
}

public class DeleteInvestorHandler : IRequestHandler<DeleteInvestorCommand, bool>
{
    public Task<bool> Handle(DeleteInvestorCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}