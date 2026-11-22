using Javaneh.Domain.Entities.Logs;

namespace Javaneh.Application.Interface.Services;

public interface ILogService
{
    Task AddLogAsync(Log log);
}