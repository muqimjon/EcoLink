using EcoLink.ApiService.Models.Representation;
using EcoLink.ApiService.Interfaces.Representation;
using System.Net;

namespace EcoLink.ApiService.Services.Representation;

public class RepresentationAppService(HttpClient client) : IRepresentationAppService
{
    public async Task<RepresentationAppDto> AddAsync(RepresentationAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<RepresentationAppDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<RepresentationAppDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode || response.StatusCode is HttpStatusCode.NoContent)
            return default!;

        return (await response.Content.ReadFromJsonAsync<RepresentationAppDto>(cancellationToken: cancellationToken))!;
    }
}
