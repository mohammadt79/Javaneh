
using Javaneh.Application.DTOs.Auth;

namespace Javaneh.Application.Interface.Services.Auth;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);

    Task<AuthResult> LoginAsync(LoginRequest request);
}
