namespace EcoLink.ApiService.Services;

public class UserService(HttpClient client) : IUserService
{
    public async Task<UserDto> AddAsync(UserDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<UserDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<int> UpdateAsync(UserDto dto, CancellationToken cancellationToken)
    {
        using var multipartFormContent = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PutAsync("update", multipartFormContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken))!;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.DeleteAsync($"delete/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<bool>(cancellationToken: cancellationToken))!;
    }

    public async Task<UserDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get-by-telegram-id/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<UserDto>(cancellationToken: cancellationToken))!;
    }
}
