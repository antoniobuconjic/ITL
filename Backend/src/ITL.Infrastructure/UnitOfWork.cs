using ITL.Domain;
using ITL.Domain.Repositories;
using ITL.Infrastructure.Database;
using System.Threading.Tasks;

namespace ITL.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public IPermissionRepository PermissionRepository { get; set; }
    public IPermissionTypeRepository PermissionTypeRepository { get; set; }
    public IUserRepository UserRepository { get; set; }

    private readonly KCTestContext _kCTestContext;

    public UnitOfWork(KCTestContext kCTestContext, IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository, IUserRepository userRepository)
    {
        _kCTestContext = kCTestContext;

        PermissionRepository = permissionRepository;
        PermissionTypeRepository = permissionTypeRepository;
        UserRepository = userRepository;
    }

    public async Task<int> SaveAsync() => await _kCTestContext.SaveChangesAsync();

    public void Dispose() => _kCTestContext.Dispose();
}
