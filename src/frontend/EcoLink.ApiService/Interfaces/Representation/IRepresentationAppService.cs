using EcoLink.ApiService.Models.Representation;

namespace EcoLink.ApiService.Interfaces.Representation;

public interface IRepresentationAppService
{
    Task<RepresentationAppDto> AddAsync(RepresentationAppDto dto, CancellationToken cancellationToken);
    Task<RepresentationAppDto> GetAsync(long userId, CancellationToken cancellationToken);
}
