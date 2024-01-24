using EcoLink.ApiService.Models.Entrepreneurship;
using EcoLink.ApiService.Interfaces.Entrepreneurship;

namespace EcoLink.ApiService.Services.Entrepreneurship;

public class EntrepreneurshipAppService(HttpClient client) : IEntrepreneurshipAppService
{
    public async Task<EntrepreneurshipAppDto> AddAsync(EntrepreneurshipAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<EntrepreneurshipAppDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<EntrepreneurshipAppDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<EntrepreneurshipAppDto>(cancellationToken: cancellationToken))!;
    }
}
