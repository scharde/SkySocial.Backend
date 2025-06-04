using DAL;
using DAL.Entity;
using DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace API.User;

public interface IUserResource
{
    Task<UserModelResponse> Get(Guid userId);
}

public partial class UserResource(UserManager<ApplicationUser> userManager) : IUserResource
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

}