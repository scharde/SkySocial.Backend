using DAL.Model;

namespace API.User;

public partial class UserResource
{
    public async Task<UserModelResponse> Get(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            throw new NotFoundException("User not found");

        return new UserModelResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Avatar = "",
            Title = user.Title
        };
    }
}