using EcoLink.ApiService.Models.Investment;

namespace EcoLink.ApiService.Interfaces.Investment;

public interface IInvestmentAppService
{
    Task<InvestmentAppDto> AddAsync(InvestmentAppDto dto, CancellationToken cancellationToken);
    Task<InvestmentAppDto> UpdateStatusAsync(long userId, CancellationToken cancellationToken);
    Task<InvestmentAppDto> GetLastAsync(long userId, CancellationToken cancellationToken);
}
