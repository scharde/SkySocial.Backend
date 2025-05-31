using DAL.Entity;
using DAL.Model;

namespace API.Auth;

public partial class AuthResource
{
    public async Task<string> ExternalLoginAsync(ExternalLoginDto dto)
    {
        // dto.Provider: "Google" or "Microsoft"
        // dto.Email: from OAuth token
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            user = new ApplicationUser() { Email = dto.Email, FirstName = dto.Email, LastName = ""};
            await _userManager.CreateAsync(user);
        }

        return "";
    }
}
