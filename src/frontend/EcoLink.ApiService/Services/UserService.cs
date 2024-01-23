using EcoLink.ApiService.Models.Users;

namespace EcoLink.ApiService.Services;

public class UserService(HttpClient client) : IUserService
{
    public async Task<Response<UserDto>> AddAsync(UserDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync(requestUri: "create-with-return",
            content: content, 
            cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<UserDto>>(cancellationToken: cancellationToken))!;
    }

    public async Task<Response<int>> UpdateAsync(UserDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PutAsync(requestUri: "update",
            content: content,
            cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<int>>(cancellationToken: cancellationToken))!;
    }

    public async Task<Response<bool>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.DeleteAsync(requestUri: $"delete/{id}", cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<bool>>(cancellationToken: cancellationToken))!;
    }

    public async Task<Response<UserDto>> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync(requestUri: $"get/{id}", cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<UserDto>>(cancellationToken: cancellationToken))!;
    }

    public async Task<Response<UserDto>> GetByTelegramIdAsync(long telegramId, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync(requestUri: $"get-by-telegram-id/{telegramId}", cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<UserDto>>(cancellationToken: cancellationToken))!;
    }

    public async Task<Response<IEnumerable<UserDto>>>GetAllAsync(CancellationToken cancellationToken)
    {
        #region Pagination
        //var queryParams = new Dictionary<string, string>
        //{
        //    { nameof(@params.PageIndex), @params.PageIndex.ToString() },
        //    { nameof(@params.PageSize), @params.PageSize.ToString() },
        //    { nameof(search), search }
        //};
        //var queryString = string.Join("&", queryParams.Where(p => !string.IsNullOrEmpty(p.Value)).Select(p => $"{p.Key}={p.Value}"));
        #endregion

        using var response = await client.GetAsync(requestUri: $"get-all", cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<IEnumerable<UserDto>>>(cancellationToken: cancellationToken))!;
    }
}