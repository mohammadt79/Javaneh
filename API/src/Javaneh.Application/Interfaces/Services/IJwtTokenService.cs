using Javaneh.Domain.Entities;

namespace Javaneh.Application.Interface.Services.Auth;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(User user);
}