using ITL.Domain.Common;
using ITL.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITL.Domain.Services;

public interface IUserService
{
    Task<UserDto> AddUser(UserDto userDto);
    Task UpdateUser(UserDto userDto);
    Task DeleteUser(int userId);
    Task<UserDto> GetUser(int userId);
    Task<IEnumerable<UserDto>> GetUsers();
    Task<IEnumerable<UserDto>> GetUsers(Pagination pagination);
}
