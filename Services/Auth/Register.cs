using DAL.Model;

namespace API.Auth;

public partial class AuthResource
{
    public async Task<string> RegisterAsync(string email, string password)
    {
        var user = new ApplicationUser { Email = email, UserName = email };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded) throw new Exception("Registration failed");

        return GenerateJwtToken(user);
    }
}
