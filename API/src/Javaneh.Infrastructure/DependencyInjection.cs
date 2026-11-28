using Javaneh.Application.Interface.Repository.Auth;
using Javaneh.Application.Interface.Repository.Logs;
using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Infrastructure.Persistence.Repositories;
using Javaneh.Infrastructure.Persistence.Repositories.Auth;
using Javaneh.Infrastructure.Persistence.Repositories.Logs;
using Javaneh.Infrastructure.Persistence.Repositories.UsersSessionRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Javaneh.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        // Register Application Services
        services.AddScoped<IUsersRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserSessionsRepository, UsersSessionRepository>();
        services.AddScoped<ILogRepository, LogRepository>();

        return services;
    }
}