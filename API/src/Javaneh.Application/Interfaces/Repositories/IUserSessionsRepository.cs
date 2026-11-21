using Javaneh.Domain.Entities;

namespace Javaneh.Application.Interface.Repository.Users;

public interface IUserSessionsRepository : IRepository<UserSessions>
{
    Task<UserSessions?> GetActiveSessionByUserIdAsync(Guid userId);
}