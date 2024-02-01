using EcoLink.ApiService.Models.Users;
using EcoLink.ApiService.Interfaces.Users;

namespace EcoLink.ApiService.Services.Users;

public class UserService(HttpClient client, ILogger<UserService> logger) : IUserService
{
    public async Task<UserDto> AddAsync(UserDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create-with-return", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<UserDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<int> UpdateAsync(UserDto dto, CancellationToken cancellationToken)
    {
        using var multipartFormContent = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PutAsync("update", multipartFormContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<int>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.DeleteAsync($"delete/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<bool>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }
   
    public async Task<UserDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get-by-telegram-id/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
                return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<UserDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }
}
