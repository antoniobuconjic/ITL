using NUnit.Framework;
using Moq;
using ITL.Domain.Entities;
using ITL.Domain.Services;
using System.Threading.Tasks;
using ITL.Domain;
using ITL.Domain.Repositories;
using AutoMapper;
using ITL.Application.Services;
using System;
using ITL.Domain.DTOs;
using ITL.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace ITL.Tests.Services;


public class UserTests
{
    [Test]
    public async Task GetUserById_ReturnsUser_WhenUserExists()
    {
        var user = new User { Id = 1, Name = "Test User" };
        var userDto = new UserDto { Id = 1, Name = "Test User" };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        var userRepoMock = new Mock<IUserRepository>();
        mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDto);
        unitOfWorkMock.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        userRepoMock.Setup(repo => repo.ExistAsync(x => x.Id == 1)).Returns(Task.FromResult(true));
        userRepoMock.Setup(repo => repo.GetByIdAsync(1, null)).Returns(Task.FromResult(user));

        var service = new UserService(mapperMock.Object, unitOfWorkMock.Object);

        var result = await service.GetUser(1);
        Assert.That(user.Id, Is.EqualTo(result.Id));
        Assert.That(user.Name, Is.EqualTo(result.Name));
    }

    [Test]
    public void GetUserById_ThrowsNotFoundException_WhenUserDoesNotExist()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(false);
        unitOfWorkMock.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);

        var service = new UserService(null, unitOfWorkMock.Object);

        Assert.ThrowsAsync<NotFoundException>(() => service.GetUser(1), "User not found");
    }
}