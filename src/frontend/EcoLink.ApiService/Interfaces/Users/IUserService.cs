using EcoLink.ApiService.Models.Users;

namespace EcoLink.ApiService.Interfaces.Users;

public interface IUserService
{
    Task<UserDto> AddAsync(UserDto dto, CancellationToken cancellationToken);
    Task<int> UpdateAsync(UserDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken);
    Task<UserDto> GetAsync(long id, CancellationToken cancellationToken);
}