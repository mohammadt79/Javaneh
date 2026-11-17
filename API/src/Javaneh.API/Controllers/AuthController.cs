using Javaneh.Application.DTOs.Auth;
using Javaneh.Application.Interface.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Javaneh.API.Controllers;

public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                Errors = result.Errors
            });
        }

        return Ok("User registered successfully");
    }
}