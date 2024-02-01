using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.ApiService.Interfaces.ProjectManagement;

public interface IProjectManagementAppService
{
    Task<ProjectManagementAppDto> AddAsync(ProjectManagementAppDto dto, CancellationToken cancellationToken);
    Task<ProjectManagementAppDto> UpdateStatusAsync(long userId, CancellationToken cancellationToken);
    Task<ProjectManagementAppDto> GetLastAsync(long userId, CancellationToken cancellationToken);
}
