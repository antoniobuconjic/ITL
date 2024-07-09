using ITL.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITL.Infrastructure.Database;

public class KCTestContext : DbContext
{
    public KCTestContext(DbContextOptions<KCTestContext> options) : base(options)
    {

    }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PermissionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.Seed();
    }
}

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermissionType>().HasData(
            new PermissionType { Id = 1, Description = "Enfermedad" },
            new PermissionType { Id = 2, Description = "Diligencias" },
            new PermissionType { Id = 3, Description = "Otros" }
        );
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@gmail.com", Password = "123456", Role = "admin" }
        );
    }
}
