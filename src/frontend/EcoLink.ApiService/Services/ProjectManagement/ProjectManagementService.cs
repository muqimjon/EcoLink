using EcoLink.ApiService.Interfaces.ProjectManagement;
using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.ApiService.Services.ProjectManagement;

public class ProjectManagementService(HttpClient client) : IProjectManagementService
{
    public async Task<ProjectManagementDto> AddAsync(ProjectManagementDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<ProjectManagementDto>(cancellationToken: cancellationToken))!;
    }
}