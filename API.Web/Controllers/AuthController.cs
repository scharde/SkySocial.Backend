using API.Auth;
using DAL.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class AuthController(
    IAuthResource authResource,
    SignInManager<ApplicationUser> signInManager
) : ControllerBase
{
    private readonly IAuthResource _authResource = authResource;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var token = await _authResource.RegisterAsync(request);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] RegisterRequest request)
    {
        var token = await _authResource.LoginAsync(request.Email, request.Password);
        return Ok(new { token });
    }

    [HttpGet("google-login")]
    public IActionResult LoginGoogle([FromQuery] string returnUrl, [FromServices] LinkGenerator linkGenerator)
    {
        var callbackUrl = linkGenerator.GetPathByName(HttpContext, "GoogleLoginCallback")
                          ?? "/api/auth/google-login-callback";

        var properties = _signInManager.ConfigureExternalAuthenticationProperties(
            "Google",
            $"{callbackUrl}?returnUrl={Uri.EscapeDataString(returnUrl)}"
        );

        return Challenge(properties, new[] { "Google" });
    }

    [HttpGet("google-login-callback", Name = "GoogleLoginCallback")]
    public async Task<IActionResult> GoogleLoginCallback([FromQuery] string returnUrl = "/")
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        await _authResource.LoginWithGoogleAsync(result.Principal);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return Redirect(returnUrl);
        }
    }
}