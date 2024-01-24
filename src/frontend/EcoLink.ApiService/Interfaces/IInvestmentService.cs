using EcoLink.ApiService.Models;

namespace EcoLink.ApiService.Interfaces;

public interface IInvestmentService
{
    Task<InvestmentDto> AddAsync(InvestmentDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(InvestmentDto dto, CancellationToken cancellationToken);
    Task<InvestmentDto> GetAsync(long id, CancellationToken cancellationToken);
}
