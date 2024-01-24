using EcoLink.ApiService.Models.Investment;

namespace EcoLink.ApiService.Interfaces.Investment;

public interface IInvestmentService
{
    Task<InvestmentDto> AddAsync(InvestmentDto dto, CancellationToken cancellationToken);
}
