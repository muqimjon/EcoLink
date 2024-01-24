namespace EcoLink.ApiService.Interfaces;

public interface IEntrepreneurshipService
{
    Task<EntrepreneurshipAppDto> AddAsync(EntrepreneurshipAppDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(EntrepreneurshipAppDto dto, CancellationToken cancellationToken);
    Task<EntrepreneurshipAppDto> GetAsync(long id, CancellationToken cancellationToken);
}
