namespace EcoLink.ApiService.Interfaces;

public interface IInvestmentService
{
    Task<InvestmentAppDto> AddAsync(InvestmentAppDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(InvestmentAppDto dto, CancellationToken cancellationToken);
    Task<InvestmentAppDto> GetAsync(long id, CancellationToken cancellationToken);
}
