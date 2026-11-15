using Javaneh.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Javaneh.Application.Interface.Repository.Users;

namespace Javaneh.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUsersRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}