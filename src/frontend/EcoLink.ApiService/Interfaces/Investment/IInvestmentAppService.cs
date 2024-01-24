using EcoLink.ApiService.Models.Investment;

namespace EcoLink.ApiService.Interfaces.Investment;

public interface IInvestmentAppService
{
    Task<InvestmentAppDto> AddAsync(InvestmentAppDto dto, CancellationToken cancellationToken);
    Task<InvestmentAppDto> GetAsync(long userId, CancellationToken cancellationToken);
}
