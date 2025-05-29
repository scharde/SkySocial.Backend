namespace API.Auth;

public partial class AuthResource
{
    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new UnauthorizedAccessException("Invalid login");

        return GenerateJwtToken(user);
    }
}
