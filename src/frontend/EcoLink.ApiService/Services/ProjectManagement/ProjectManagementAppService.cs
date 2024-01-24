using EcoLink.ApiService.Interfaces.ProjectManagement;
using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.ApiService.Services.ProjectManagement;

public class ProjectManagementAppService(HttpClient client) : IProjectManagementAppService
{
    public async Task<ProjectManagementAppDto> AddAsync(ProjectManagementAppDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<ProjectManagementAppDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<ProjectManagementAppDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<ProjectManagementAppDto>(cancellationToken: cancellationToken))!;
    }
}
