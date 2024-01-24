using EcoLink.ApiService.Interfaces.Investment;
using EcoLink.ApiService.Models.Investment;

namespace EcoLink.ApiService.Services.Investment;

public class InvestmentService(HttpClient client) : IInvestmentService
{
    public async Task<InvestmentDto> AddAsync(InvestmentDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<InvestmentDto>(cancellationToken: cancellationToken))!;
    }
}