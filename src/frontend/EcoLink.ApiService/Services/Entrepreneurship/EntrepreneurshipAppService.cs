using EcoLink.ApiService.Models.Entrepreneurship;
using EcoLink.ApiService.Interfaces.Entrepreneurship;

namespace EcoLink.ApiService.Services.Entrepreneurship;

public class EntrepreneurshipAppService(HttpClient client, ILogger<EntrepreneurshipAppService> logger) : IEntrepreneurshipAppService
{
    public async Task<EntrepreneurshipAppDto> AddAsync(EntrepreneurshipAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<EntrepreneurshipAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<EntrepreneurshipAppDto> UpdateStatusAsync(long userId, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(new { UserId = userId });
        using var response = await client.PostAsync("update-status", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<EntrepreneurshipAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<EntrepreneurshipAppDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        var s = response.StatusCode;
        if (!response.IsSuccessStatusCode || response.StatusCode is System.Net.HttpStatusCode.NoContent)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<EntrepreneurshipAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }
}
