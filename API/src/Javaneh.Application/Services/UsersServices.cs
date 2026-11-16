using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Application.Interface.services.Users;
using Javaneh.Domain.Entities;

public class UsersServices : IUserservices
{
    private readonly IUsersRepository _usersRepository;

    public UsersServices(IUsersRepository  usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _usersRepository.GetAllAsync();
    }
}