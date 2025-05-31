using DAL.Entity;
using DAL.Model;

namespace API.Auth;

public partial class AuthResource
{
    public async Task<(string jwtToken, DateTime expiresAtUtc)> RegisterAsync(RegisterRequest registerRequest)
    {
        var userExists = await _userManager.FindByEmailAsync(registerRequest.Email) != null;
        if (userExists)
        {
            throw new Exception($"User with email: {registerRequest.Email} already exists");
        }
        
        var user = ApplicationUser.Create(registerRequest.Email, registerRequest.FirstName, registerRequest.LastName);
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, registerRequest.Password);

        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded) 
            throw new Exception("Registration failed");

        return _tokenProcessor.GenerateJwtToken(user);
    }
}
