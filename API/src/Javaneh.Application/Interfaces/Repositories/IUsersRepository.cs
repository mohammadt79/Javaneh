using Javaneh.Domain.Entities;

namespace Javaneh.Application.Interface.Repository.Users;

public interface IUsersRepository
{
    public Task<List<User>> GetAllAsync();

    public Task<User?> GetByEmailAsync(string email);
}