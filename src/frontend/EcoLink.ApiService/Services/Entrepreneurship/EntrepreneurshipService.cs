using EcoLink.ApiService.Interfaces.Entrepreneurship;
using EcoLink.ApiService.Models.Entrepreneurship;

namespace EcoLink.ApiService.Services.Entrepreneurship;

public class EntrepreneurshipService(HttpClient client) : IEntrepreneurshipService
{
    public async Task<EntrepreneurshipDto> AddAsync(EntrepreneurshipDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<EntrepreneurshipDto>(cancellationToken: cancellationToken))!;
    }
}