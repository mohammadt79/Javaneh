using Javaneh.Domain.Entities;

namespace Javaneh.Application.Interface.Repository.Users;

public interface IUsersRepository : IRepository<User>
{
    public Task<User?> GetByEmailAsync(string email);
}