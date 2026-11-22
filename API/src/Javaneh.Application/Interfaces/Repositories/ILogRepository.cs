using Javaneh.Domain.Entities.Logs;

namespace Javaneh.Application.Interface.Repository.Logs;

public interface ILogRepository
{
    Task AddAsync(Log log);
}