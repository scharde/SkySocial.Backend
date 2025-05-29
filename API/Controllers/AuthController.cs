using API.Auth;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthResource _authResource;

    public AuthController(IAuthResource authResource)
    {
        _authResource = authResource;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var token = await _authResource.RegisterAsync(request.Email, request.Password);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var token = await _authResource.LoginAsync(request.Email, request.Password);
        return Ok(new { token });
    }

    [HttpPost("oauth-login")]
    public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDto dto)
    {
        var token = await _authResource.ExternalLoginAsync(dto);
        return Ok(new { token });
    }
}
