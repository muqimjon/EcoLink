using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.ApiService.Interfaces.ProjectManagement;

public interface IProjectManagementAppService
{
    Task<ProjectManagementAppDto> AddAsync(ProjectManagementAppDto dto, CancellationToken cancellationToken);
    Task<ProjectManagementAppDto> GetAsync(long userId, CancellationToken cancellationToken);
}
