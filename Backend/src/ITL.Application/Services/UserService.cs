using AutoMapper;
using ITL.Domain;
using ITL.Domain.Common;
using ITL.Domain.DTOs;
using ITL.Domain.Entities;
using ITL.Domain.Exceptions;
using ITL.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITL.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> AddUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<UserDto>(user);
    }
    public async Task UpdateUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteUser(int userId)
    {
        var exists = await _unitOfWork.UserRepository.ExistAsync(x => x.Id == userId);
        if (!exists)
        {
            throw new NotFoundException("User not found");
        }

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        await _unitOfWork.UserRepository.DeleteAsync(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task<UserDto> GetUser(int userId)
    {
        var exists = await _unitOfWork.UserRepository.ExistAsync(x => x.Id == userId);
        if (!exists)
        {
            throw new NotFoundException("User not found");
        }
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<IEnumerable<UserDto>> GetUsers(Pagination pagination)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync(pagination.Skip, pagination.Limit);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
