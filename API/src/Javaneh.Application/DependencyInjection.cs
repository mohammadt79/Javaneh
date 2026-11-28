using Javaneh.Application.Interface.services.Users;
using Javaneh.Application.Interface.Services;
using Javaneh.Application.Interface.Services.Auth;
using Javaneh.Application.Services;
using Javaneh.Application.Services.Auth;
using Javaneh.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Javaneh.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Register Application Services
        services.AddScoped<IUserservices, UsersServices>();
        services.AddScoped<IUsersSessionService, UsersSessionService>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}