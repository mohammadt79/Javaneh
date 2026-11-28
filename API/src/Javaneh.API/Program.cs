
using Javaneh.API.Filters;
using Javaneh.API.Middleware;
using Javaneh.Application.Interface.Services.Auth;
using Javaneh.Infrastructure.Identity;
using Javaneh.Infrastructure.Persistence;
using Javaneh.Infrastructure.Persistence.Logs;
using Javaneh.Infrastructure.Services.Auth;
using Javaneh.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Javaneh.Application;
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
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));
var appSettings = builder.Configuration
    .GetSection("AppSettings")
    .Get<AppSettings>()!;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(appSettings.Cors.AllowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});// jwt settings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);
// Application Services

builder.Services.AddApplication();

// Applications Repositories
builder.Services.AddInfrastructure();
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
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // IssuerSigningKey = new SymmetricSecurityKey(
            //     Encoding.UTF8.GetBytes(
            //         builder.Configuration["Jwt:SecretKey"]!
            //     )
            // )
            IssuerSigningKey = new SymmetricSecurityKey(
          Convert.FromBase64String(
        builder.Configuration["Jwt:SecretKey"]!
    )
)
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandling>();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
