using EcoLink.ApiService.Models;

namespace EcoLink.ApiService.Interfaces;

public interface IRepresentationService
{
    Task<RepresentationDto> AddAsync(RepresentationDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(RepresentationDto dto, CancellationToken cancellationToken);
    Task<RepresentationDto> GetAsync(long id, CancellationToken cancellationToken);
}
