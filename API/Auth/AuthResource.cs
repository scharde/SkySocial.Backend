using System.Security.Claims;
using API.TokenProcessor;
using DAL.Entity;
using DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace API.Auth;

public interface IAuthResource
{
    Task RegisterAsync(RegisterRequest registerRequest);
    Task LoginAsync(string email, string password);
    Task<string> ExternalLoginAsync(ExternalLoginDto dto);
    Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
}

public partial class AuthResource(
    UserManager<ApplicationUser> userManager,
    IAuthTokenProcessor tokenProcessor
) : IAuthResource
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IAuthTokenProcessor _tokenProcessor = tokenProcessor;
}