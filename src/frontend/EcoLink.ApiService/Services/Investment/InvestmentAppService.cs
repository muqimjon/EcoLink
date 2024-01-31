using EcoLink.ApiService.Models.Investment;
using EcoLink.ApiService.Interfaces.Investment;

namespace EcoLink.ApiService.Services.Investment;

public class InvestmentAppService(HttpClient client, ILogger<InvestmentAppService> logger) : IInvestmentAppService
{
    public async Task<InvestmentAppDto> AddAsync(InvestmentAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<InvestmentAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<InvestmentAppDto> UpdateStatusAsync(long userId, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(new { UserId = userId, IsOld = true });
        using var response = await client.PutAsync("update-status", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<InvestmentAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<InvestmentAppDto> GetLastAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get-all-by-user-id/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode || response.StatusCode is HttpStatusCode.NoContent)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<IEnumerable<InvestmentAppDto>>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data.LastOrDefault()!;

        logger.LogInformation(message: result.Message);
        return default!;
    }
}
