using Javaneh.Application.Interface.Repository.Logs;
using Javaneh.Domain.Entities.Logs;
using Javaneh.Infrastructure.Persistence.Logs;

namespace Javaneh.Infrastructure.Persistence.Repositories.Logs;

public class LogRepository : ILogRepository
{
    private readonly LogsDbContext _context;

    public LogRepository(LogsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Log log)
    {
        await _context.Logs.AddAsync(log);
        await _context.SaveChangesAsync();
    }
}