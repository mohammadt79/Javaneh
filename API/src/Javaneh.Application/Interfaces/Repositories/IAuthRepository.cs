
using Javaneh.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Javaneh.Application.Interface.Repository.Auth;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterAsync(
        User user,
        string password
    );

    Task<User?> GetByEmailAsync(string email);

    Task<bool> CheckPasswordAsync(
        User user,
        string password
    );
}
