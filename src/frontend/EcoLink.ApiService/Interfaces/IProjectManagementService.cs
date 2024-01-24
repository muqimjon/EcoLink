namespace EcoLink.ApiService.Interfaces;

public interface IProjectManagementService
{
    Task<ProjectManagementAppDto> AddAsync(ProjectManagementAppDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(ProjectManagementAppDto dto, CancellationToken cancellationToken);
    Task<ProjectManagementAppDto> GetAsync(long id, CancellationToken cancellationToken);
}
