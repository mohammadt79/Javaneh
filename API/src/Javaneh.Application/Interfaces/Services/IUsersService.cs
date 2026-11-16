using Javaneh.Domain.Entities;

namespace Javaneh.Application.Interface.services.Users;


public interface IUserservices
{
    public Task<List<User>> GetAllUsers();
}