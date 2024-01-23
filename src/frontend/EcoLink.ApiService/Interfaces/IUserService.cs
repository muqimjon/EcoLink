using EcoLink.ApiService.Models.Users;

namespace EcoLink.ApiService.Interfaces;

public interface IUserService
{
    Task<Response<UserDto>> AddAsync(UserDto dto, CancellationToken cancellationToken);
    Task<Response<int>> UpdateAsync(UserDto dto, CancellationToken cancellationToken);
    Task<Response<bool>> DeleteAsync(long id, CancellationToken cancellationToken);
    Task<Response<UserDto>> GetAsync(long id, CancellationToken cancellationToken);
    Task<Response<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken);
}
