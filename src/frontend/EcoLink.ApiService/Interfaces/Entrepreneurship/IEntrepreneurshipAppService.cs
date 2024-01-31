using EcoLink.ApiService.Models.Entrepreneurship;

namespace EcoLink.ApiService.Interfaces.Entrepreneurship;

public interface IEntrepreneurshipAppService
{
    Task<EntrepreneurshipAppDto> AddAsync(EntrepreneurshipAppDto dto, CancellationToken cancellationToken);
    Task<EntrepreneurshipAppDto> UpdateStatusAsync(long userId, CancellationToken cancellationToken);
    Task<EntrepreneurshipAppDto> GetLastAsync(long userId, CancellationToken cancellationToken);
}
