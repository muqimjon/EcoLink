using EcoLink.ApiService.Interfaces.Investment;
using EcoLink.ApiService.Models.Investment;
using System.Net;

namespace EcoLink.ApiService.Services.Investment;

public class InvestmentAppService(HttpClient client) : IInvestmentAppService
{
    public async Task<InvestmentAppDto> AddAsync(InvestmentAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<InvestmentAppDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<InvestmentAppDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode || response.StatusCode is HttpStatusCode.NoContent)
            return default!;

        return (await response.Content.ReadFromJsonAsync<InvestmentAppDto>(cancellationToken: cancellationToken))!;
    }
}
