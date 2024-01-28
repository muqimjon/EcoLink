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

    public async Task<ProjectManagementAppDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode || response.StatusCode is HttpStatusCode.NoContent)
            return default!;

        var result = await response.Content.ReadFromJsonAsync<Response<ProjectManagementAppDto>>(cancellationToken: cancellationToken);
        if (result!.Status == 200)
            return result.Data;

        logger.LogInformation(message: result.Message);
        return default!;
    }
}
