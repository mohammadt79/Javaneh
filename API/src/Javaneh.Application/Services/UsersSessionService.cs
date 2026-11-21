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
}