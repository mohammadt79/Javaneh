
using Javaneh.Application.DTOs.Auth;
using Javaneh.Application.Interface.Repository.Auth;
using Javaneh.Application.Interface.Services.Auth;
using Javaneh.Domain.Entities;

namespace Javaneh.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth
        };

        var result = await _authRepository.RegisterAsync(
            user,
            request.Password
        );

        if (!result.Succeeded)
        {
            return new AuthResult
            {
                Succeeded = false,
                Errors = result.Errors
                 .Select(error => error.Description)
                 .ToList()
            };
        }

        return new AuthResult
        {
            Succeeded = true
        };
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        // TODO: Implement login
        return new AuthResult();
    }
}
