using EcoLink.ApiService.Models.Entrepreneurship;

namespace EcoLink.ApiService.Interfaces.Entrepreneurship;

public interface IEntrepreneurshipService
{
    Task<EntrepreneurshipDto> AddAsync(EntrepreneurshipDto dto, CancellationToken cancellationToken);
}