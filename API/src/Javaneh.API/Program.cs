using Javaneh.Application.Interface.Repository.Users;
using Javaneh.Application.Interface.services.Users;
using Javaneh.Infrastructure.Persistence;
using Javaneh.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserservices, UsersServices>();
builder.Services.AddScoped<IUsersRepository, UserRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();