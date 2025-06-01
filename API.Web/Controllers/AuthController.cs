using API.Auth;
using DAL.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IAuthResource authResource,
    SignInManager<ApplicationUser> signInManager
) : BaseController
{
    private readonly IAuthResource _authResource = authResource;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var token = await _authResource.RegisterAsync(request);
        return Ok(new { token });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] RegisterRequest request)
    {
        var token = await _authResource.LoginAsync(request.Email, request.Password);
        return Ok(new { token });
    }

    [HttpGet("google-login")]
    [AllowAnonymous]
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
    [AllowAnonymous]
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

    [HttpGet("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Ok();
    }

    [HttpGet("check-auth")]
    [Authorize]
    public IActionResult CheckAuth()
    {
        if (CurrentUserId is null)
            return Unauthorized();

        return Ok();
    }
}