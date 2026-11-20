using Javaneh.Application.Interface.Repository.Auth;
using Javaneh.Domain.Entities;
using Javaneh.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Javaneh.Infrastructure.Persistence.Repositories.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // Register
    public async Task<IdentityResult> RegisterAsync(User user, string password)
    {
        var applicationUser = new ApplicationUser
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth
        };

        return await _userManager.CreateAsync(applicationUser, password);
    }

    // Get User By Email
    public async Task<User?> GetByEmailAsync(string email)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);

        if (applicationUser is null)
            return null;

        return MapToDomainUser(applicationUser);
    }

    // Login
    public async Task<User?> GetUserByCredentialsAsync(
        string email,
        string password)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);

        if (applicationUser is null)
            return null;

        var isPasswordValid = await _userManager.CheckPasswordAsync(
            applicationUser,
            password
        );

        if (!isPasswordValid)
            return null;

        return MapToDomainUser(applicationUser);
    }

    // Mapping
    private static User MapToDomainUser(ApplicationUser applicationUser)
    {
        return new User
        {
            Id = applicationUser.Id,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            Email = applicationUser.Email!,
            PhoneNumber = applicationUser.PhoneNumber ?? string.Empty,
            DateOfBirth = applicationUser.DateOfBirth
        };
    }
}