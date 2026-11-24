
using Javaneh.API.Filters;
using Javaneh.API.Middleware;
using Javaneh.Application.Interface.Repository.Auth;
using Javaneh.Application.Interface.Repository.Logs;
using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Application.Interface.services.Users;
using Javaneh.Application.Interface.Services;
using Javaneh.Application.Interface.Services.Auth;
using Javaneh.Application.Services;
using Javaneh.Application.Services.Auth;
using Javaneh.Infrastructure.Identity;
using Javaneh.Infrastructure.Persistence;
using Javaneh.Infrastructure.Persistence.Logs;
using Javaneh.Infrastructure.Persistence.Repositories;
using Javaneh.Infrastructure.Persistence.Repositories.Auth;
using Javaneh.Infrastructure.Persistence.Repositories.Logs;
using Javaneh.Infrastructure.Persistence.Repositories.UsersSessionRepository;
using Javaneh.Infrastructure.Services;
using Javaneh.Infrastructure.Services.Auth;
using Javaneh.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalResultHandling>();
});
// Data Protection
builder.Services.AddDataProtection();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddDbContext<LogsDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("LogsConnection")
));

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});
// Swagger
builder.Services.AddSwaggerGen();
// jwt settings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);
// Application Services
builder.Services.AddScoped<IUserservices, UsersServices>();
builder.Services.AddScoped<
    IUsersSessionService,
    UsersSessionService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Applications Repositories
builder.Services.AddScoped<IUsersRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserSessionsRepository, UsersSessionRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
// ASP.NET Core Identity
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();
app.UseCors("AllowFrontend");

app.UseMiddleware<GlobalExceptionHandling>();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
