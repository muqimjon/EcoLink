using EcoLink.ApiService.Models;

namespace EcoLink.ApiService.Interfaces;

public interface IProjectManagementService
{
    Task<ProjectManagementDto> AddAsync(ProjectManagementDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(ProjectManagementDto dto, CancellationToken cancellationToken);
    Task<ProjectManagementDto> GetAsync(long id, CancellationToken cancellationToken);
}
