using Javaneh.Domain.Entities;

namespace Javaneh.Application.Interface.Services
{
    public interface IUsersSessionService
    {
        Task AddSessionService(UserSessions userSessions);

        Task<bool> IsUserLoggedIn(Guid userId);

        Task RevokeActiveSessionAsync(Guid userId);
    }
}