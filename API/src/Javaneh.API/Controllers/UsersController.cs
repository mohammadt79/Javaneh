using Javaneh.Application.Interface.services.Users;
using Javaneh.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Javaneh.API.Controllers;

public class UsersController(IUserservices usersService) : BaseApiController
{
    [Authorize]
    [HttpGet]
    public async Task<List<User>> GetUsers()
    {
        return await usersService.GetAllUsers();
    }
}