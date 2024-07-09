using ITL.Domain.Services;
using ITL.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ITL.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetUser(int userId)
    {
        var user = await _userService.GetUser(userId);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
    {
        var user = await _userService.AddUser(userDto);
        return Ok(user);
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<UserDto>> UpdateUser(UserDto userDto)
    {
        await _userService.UpdateUser(userDto);
        return Ok();
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<UserDto>> DeleteUser(int userId)
    {
        await _userService.DeleteUser(userId);
        return Ok();
    }

}
