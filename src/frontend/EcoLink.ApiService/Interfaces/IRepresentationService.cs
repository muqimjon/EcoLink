namespace EcoLink.ApiService.Interfaces;

public interface IRepresentationService
{
    Task<RepresentationAppDto> AddAsync(RepresentationAppDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(RepresentationAppDto dto, CancellationToken cancellationToken);
    Task<RepresentationAppDto> GetAsync(long id, CancellationToken cancellationToken);
}
