using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Domain.Entities;
using Javaneh.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Javaneh.Infrastructure.Persistence.Repositories;

public class UserRepository : IUsersRepository
{
    private readonly DbSet<ApplicationUser> _dbSet;

    public UserRepository(AppDbContext context)
    {
        _dbSet = context.Set<ApplicationUser>();
    }

    public async Task<List<User>> GetAllAsync()
    {
        var users = await _dbSet.ToListAsync();
        return users.Select(MapToUser).ToList();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var appUser = await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email);

        return appUser is null ? null : MapToUser(appUser);
    }

    private static User MapToUser(ApplicationUser user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email ?? string.Empty,
        PhoneNumber = user.PhoneNumber ?? string.Empty,
        DateOfBirth = user.DateOfBirth
    };
}