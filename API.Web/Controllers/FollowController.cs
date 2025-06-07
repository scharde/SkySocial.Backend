using API.Follow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FollowController(IFollowResource followResource) : BaseController
{
    private readonly IFollowResource _followResource = followResource;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (CurrentUserId == null)
            return Unauthorized();
        
        var result = await _followResource.Get(CurrentUserId.Value);
        return Ok(result);
    }
    
    [HttpPost("{followeeId}/toggle")]
    public async Task<IActionResult> FollowUser(Guid followeeId)
    {
        if (CurrentUserId == null)
            return Unauthorized();
        
        var result = await _followResource.Post(followeeId, CurrentUserId.Value);

        return Ok(result);
    }
}