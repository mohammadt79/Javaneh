
using Javaneh.Domain.Entities;
using Javaneh.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Javaneh.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<
    ApplicationUser,
    IdentityRole<Guid>,
    Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<UserSessions> UserSessions => Set<UserSessions>();

}
