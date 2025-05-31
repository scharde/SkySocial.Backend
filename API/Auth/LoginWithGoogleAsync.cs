using System.Security.Claims;
using DAL.Entity;
using DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace API.Auth;

public partial class AuthResource
{
    public async Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new Exception("Google: ClaimsPrincipal is null");
        }
        
        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            throw new Exception("Google: Email is null");
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            var newUser = new ApplicationUser()
            {
                UserName = email,
                Email = email,
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser);
            if (!result.Succeeded)
            {
                throw new Exception("GoogleGoogle: Unable to create user: ");
            }
            
            user = newUser;
            
            var info = new UserLoginInfo("Google",
                claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
                "Google");

            var loginResult = await _userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
            {
                throw new Exception("Google: Unable to login user: ");
            }
        }
        
        var (jwtToken, expirationDateInUtc) = _tokenProcessor.GenerateJwtToken(user);
        var refreshTokenValue = _tokenProcessor.GenerateRefreshToken();
        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

        await _userManager.UpdateAsync(user);
        
        _tokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _tokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }
}