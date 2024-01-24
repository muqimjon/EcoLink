using EcoLink.ApiService.Models;

namespace EcoLink.ApiService.Interfaces;

public interface IEntrepreneurshipService
{
    Task<EntrepreneurshipDto> AddAsync(EntrepreneurshipDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(EntrepreneurshipDto dto, CancellationToken cancellationToken);
    Task<EntrepreneurshipDto> GetAsync(long id, CancellationToken cancellationToken);
}
