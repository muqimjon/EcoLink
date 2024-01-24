using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.ApiService.Interfaces.ProjectManagement;

public interface IProjectManagementService
{
    Task<ProjectManagementDto> AddAsync(ProjectManagementDto dto, CancellationToken cancellationToken);
}