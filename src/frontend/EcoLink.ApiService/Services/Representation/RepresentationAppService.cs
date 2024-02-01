using EcoLink.ApiService.Models.Representation;
using EcoLink.ApiService.Interfaces.Representation;

namespace EcoLink.ApiService.Services.Representation;

public class RepresentationAppService(HttpClient client, ILogger<RepresentationAppService> logger) : IRepresentationAppService
{
    public async Task<RepresentationAppDto> AddAsync(RepresentationAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<RepresentationAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<RepresentationAppDto> UpdateStatusAsync(long userId, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(new { UserId = userId, IsOld = true });
        using var response = await client.PutAsync("update-status", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<RepresentationAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<RepresentationAppDto> GetLastAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get-all-by-user-id/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode || response.StatusCode is HttpStatusCode.NoContent)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<List<RepresentationAppDto>>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data.LastOrDefault()!;

        logger.LogInformation(message: result.Message);
        return default!;
    }
}
