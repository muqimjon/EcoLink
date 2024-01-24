namespace MedX.ApiService.Services;

public class EntrepreneurshipService(HttpClient client) : IEntrepreneurshipService
{
    public async Task<EntrepreneurshipAppDto> AddAsync(EntrepreneurshipAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<EntrepreneurshipAppDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<int> UpdateAsync(EntrepreneurshipAppDto dto, CancellationToken cancellationToken)
    {
        using var multipartFormContent = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PutAsync("update", multipartFormContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken))!;
    }

    public async Task<EntrepreneurshipAppDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<EntrepreneurshipAppDto>(cancellationToken: cancellationToken))!;
    }
}