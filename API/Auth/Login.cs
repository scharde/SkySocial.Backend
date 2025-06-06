namespace API.Auth;

public partial class AuthResource
{
    public async Task LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new UnauthorizedAccessException("Invalid login");

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
