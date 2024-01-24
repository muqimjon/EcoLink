using EcoLink.ApiService.Models.Representation;

namespace EcoLink.ApiService.Interfaces.Representation;

public interface IRepresentationService
{
    Task<RepresentationDto> AddAsync(RepresentationDto dto, CancellationToken cancellationToken);
}