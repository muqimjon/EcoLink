using EcoLink.ApiService.Interfaces.Representation;
using EcoLink.ApiService.Models.Representation;

namespace EcoLink.ApiService.Services.Representation;

public class RepresentationService(HttpClient client) : IRepresentationService
{
    public async Task<RepresentationDto> AddAsync(RepresentationDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<RepresentationDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<RepresentationDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<RepresentationDto>(cancellationToken: cancellationToken))!;
    }
}