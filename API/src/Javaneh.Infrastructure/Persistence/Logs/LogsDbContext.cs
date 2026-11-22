using Javaneh.Domain.Entities.Logs;
using Microsoft.EntityFrameworkCore;

namespace Javaneh.Infrastructure.Persistence.Logs;

public class LogsDbContext : DbContext
{
    public LogsDbContext(DbContextOptions<LogsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Log> Logs { get; set; }
}