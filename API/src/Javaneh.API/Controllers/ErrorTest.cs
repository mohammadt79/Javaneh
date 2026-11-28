
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Javaneh.API.Controllers;


public class TestController : BaseApiController
{
    // تست پاسخ موفق 200
    [Authorize]
    [HttpGet("success")]
    public IActionResult Success()
    {
        return Ok(new
        {
            Id = 1,
            Name = "Mohammad"
        });
    }

    // تست پاسخ 201
    [Authorize]
    [HttpPost("created")]
    public IActionResult CreatedTest()
    {
        return StatusCode(201, new
        {
            Id = 2,
            Name = "New User"
        });
    }

    // تست Exception با وضعیت 404
    [Authorize]
    [HttpGet("not-found")]
    public IActionResult NotFoundTest()
    {
        throw new KeyNotFoundException();
    }

    // تست Exception با وضعیت 403
    [Authorize]
    [HttpGet("forbidden")]
    public IActionResult ForbiddenTest()
    {
        throw new UnauthorizedAccessException();
    }

    // تست Exception با وضعیت 400
    [Authorize]
    [HttpGet("bad-request")]
    public IActionResult BadRequestTest()
    {
        throw new ArgumentException();
    }

    // تست Exception با وضعیت 500
    [Authorize]
    [HttpGet("server-error")]
    public IActionResult ServerErrorTest()
    {
        throw new Exception("Test server error");
    }
}
