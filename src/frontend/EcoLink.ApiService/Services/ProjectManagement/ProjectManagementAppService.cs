using EcoLink.ApiService.Models.ProjectManagement;
using EcoLink.ApiService.Interfaces.ProjectManagement;

namespace EcoLink.ApiService.Services.ProjectManagement;

public class ProjectManagementAppService(HttpClient client, ILogger<ProjectManagementAppService> logger) : IProjectManagementAppService
{
    public async Task<ProjectManagementAppDto> AddAsync(ProjectManagementAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<ProjectManagementAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<ProjectManagementAppDto> UpdateStatusAsync(long userId, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(new { UserId = userId });
        using var response = await client.PutAsync("update-status", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<ProjectManagementAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }

    public async Task<ProjectManagementAppDto> GetLastAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get-all-by-user-id/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode || response.StatusCode is HttpStatusCode.NoContent)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<IEnumerable<ProjectManagementAppDto>>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data.LastOrDefault()!;

        logger.LogInformation(message: result.Message);
        return default!;
    }
}
