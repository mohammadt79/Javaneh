using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Domain.Entities;
using Javaneh.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Javaneh.Infrastructure.Persistence.Repositories.UsersSessionRepository;

public class UsersSessionRepository : Repository<UserSessions>, IUserSessionsRepository
{
    public UsersSessionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserSessions?> GetActiveSessionByUserIdAsync(Guid userId)
    {
        return await _dbSet.FirstOrDefaultAsync(s =>
            s.UserId == userId &&
            s.RevokedAt == null &&
            s.ExpiresAt > DateTime.UtcNow
        );
    }
}