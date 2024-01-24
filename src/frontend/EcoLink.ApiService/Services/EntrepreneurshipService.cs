namespace MedX.ApiService.Services;

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

    public async Task<int> UpdateAsync(EntrepreneurshipDto dto, CancellationToken cancellationToken)
    {
        using var multipartFormContent = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PutAsync("update", multipartFormContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken))!;
    }

    public async Task<EntrepreneurshipDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<EntrepreneurshipDto>(cancellationToken: cancellationToken))!;
    }
}