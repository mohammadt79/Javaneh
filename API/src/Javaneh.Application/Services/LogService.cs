using Javaneh.Application.Interface.Services;
using Javaneh.Application.Interface.Repository.Logs;
using Javaneh.Domain.Entities.Logs;

namespace Javaneh.Infrastructure.Services;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;

    public LogService(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task AddLogAsync(Log log)
    {
        await _logRepository.AddAsync(log);
    }
}