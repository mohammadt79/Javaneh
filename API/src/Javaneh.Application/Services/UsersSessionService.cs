using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Application.Interface.Services;
using Javaneh.Domain.Entities;

namespace Javaneh.Application.Services;

public class UsersSessionService : IUsersSessionService
{
    private readonly IUserSessionsRepository _usersSessionsRepository;

    public UsersSessionService(IUserSessionsRepository usersSessionsRepository)
    {
        _usersSessionsRepository = usersSessionsRepository;
    }

    public async Task AddSessionService(UserSessions userSessions)
    {
        await _usersSessionsRepository.AddAsync(userSessions);
    }

    public async Task<bool> IsUserLoggedIn(Guid userId)
    {
        var session = await _usersSessionsRepository
            .GetActiveSessionByUserIdAsync(userId);

        return session != null;
    }

    public async Task RevokeActiveSessionAsync(Guid userId)
    {
        var session = await _usersSessionsRepository
            .GetActiveSessionByUserIdAsync(userId);

        if (session is null)
            return;

        session.RevokedAt = DateTime.UtcNow;

        _usersSessionsRepository.Update(session);
    }
}