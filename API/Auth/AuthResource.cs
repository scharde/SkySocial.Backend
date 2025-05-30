using System.Security.Claims;
using API.TokenProcessor;
using DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace API.Auth;

public interface IAuthResource
{
    Task<(string jwtToken, DateTime expiresAtUtc)> RegisterAsync(RegisterRequest registerRequest);
    Task<(string jwtToken, DateTime expiresAtUtc)> LoginAsync(string email, string password);
    Task<string> ExternalLoginAsync(ExternalLoginDto dto);
    Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
}

public partial class AuthResource : IAuthResource
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthTokenProcessor _tokenProcessor;

    public AuthResource
    (
        UserManager<ApplicationUser> userManager,
        IAuthTokenProcessor tokenProcessor
    )
    {
        _userManager = userManager;
        _tokenProcessor = tokenProcessor;
    }
}
