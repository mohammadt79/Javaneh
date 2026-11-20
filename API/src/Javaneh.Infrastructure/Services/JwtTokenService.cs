using Javaneh.Application.Interface.Services.Auth;
using Javaneh.Domain.Entities;
using Javaneh.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Javaneh.Infrastructure.Services.Auth;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public Task<string> GenerateTokenAsync(User user)
    {
        // 1. Define Claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

        // 2. Decode Secret Key
        var keyBytes = Convert.FromBase64String(
            _jwtSettings.SecretKey
        );

        if (keyBytes.Length < 32)
        {
            throw new InvalidOperationException(
                "JWT SecretKey must be at least 32 bytes."
            );
        }

        var key = new SymmetricSecurityKey(keyBytes);

        // 3. Create Signing Credentials
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        // 4. Create JWT
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _jwtSettings.ExpirationMinutes
            ),
            signingCredentials: credentials
        );

        // 5. Convert JWT to String
        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return Task.FromResult(tokenString);
    }
}