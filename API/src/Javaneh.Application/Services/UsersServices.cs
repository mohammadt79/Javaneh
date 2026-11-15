using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Domain.Entities;

public class UsersServices
{
    private readonly IUsersRepository _usersRepository;

    public UsersServices(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _usersRepository.GetAllAsync();
    }
}