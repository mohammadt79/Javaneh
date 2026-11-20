using Javaneh.Application.DTOs.Auth;
using Javaneh.Application.Interface.Repository.Auth;
using Javaneh.Application.Interface.Services.Auth;
using Javaneh.Domain.Entities;

namespace Javaneh.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IAuthRepository authRepository,
        IJwtTokenService jwtTokenService)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
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
        var user = await _authRepository.GetUserByCredentialsAsync(
            request.Email,
            request.Password
        );

        if (user is null)
        {
            return new AuthResult
            {
                Succeeded = false,
                Errors = new List<string>
                {
                    "Invalid email or password."
                }
            };
        }

        var token = await _jwtTokenService.GenerateTokenAsync(user);

        return new AuthResult
        {
            Succeeded = true,
            Token = token
        };
    }
}