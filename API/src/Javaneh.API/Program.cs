
using Javaneh.Application.Interface.Repository.Auth;
using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Application.Interface.services.Users;
using Javaneh.Application.Interface.Services.Auth;
using Javaneh.Application.Services.Auth;
using Javaneh.Infrastructure.Identity;
using Javaneh.Infrastructure.Persistence;
using Javaneh.Infrastructure.Persistence.Repositories;
using Javaneh.Infrastructure.Persistence.Repositories.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Data Protection
builder.Services.AddDataProtection();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Swagger
builder.Services.AddSwaggerGen();

// Application Services
builder.Services.AddScoped<IUserservices, UsersServices>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Applications Repositories
builder.Services.AddScoped<IUsersRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
// ASP.NET Core Identity
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
