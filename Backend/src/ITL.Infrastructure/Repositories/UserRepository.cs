using ITL.Domain.Entities;
using ITL.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITL.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbSet<User> users) : base (users)
    {
    }
}
