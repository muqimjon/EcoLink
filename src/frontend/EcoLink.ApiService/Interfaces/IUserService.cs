using EcoLink.ApiService.Models.Users;

namespace EcoLink.ApiService.Interfaces;

public interface IUserService
{
    Task<Response<UserDto>> AddAsync(EmployeeCreationDto dto);
    Task<Response<EmployeeResultDto>> UpdateAsync(EmployeeUpdateDto dto);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<EmployeeResultDto>> GetAsync(long id);
    Task<Response<IEnumerable<EmployeeResultDto>>> GetAllAsync(PaginationParams @params, string search = null!);
}
