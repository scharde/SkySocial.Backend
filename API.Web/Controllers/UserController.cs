using API.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(IUserResource userResource): BaseController
{

    private readonly IUserResource _userResource = userResource;
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (CurrentUserId == null)
            return Unauthorized();
        
        var result =await _userResource.Get(CurrentUserId.Value);
        return Ok(result);
    }
}