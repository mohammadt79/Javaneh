using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Domain.Entities;
using Javaneh.Infrastructure.Persistence;

namespace Javaneh.Infrastructure.Persistence.Repositories.UsersSessionRepository;

public class UsersSessionRepository : Repository<UserSessions>, IUserSessionsRepository
{
    public UsersSessionRepository(AppDbContext context) : base(context)
    {
    }
}