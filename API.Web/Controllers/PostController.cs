using System.Security.Claims;
using API.Post;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class PostController(IPostResource postResource) : ControllerBase
{
    private readonly IPostResource _postResource = postResource;

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userId, out Guid userGuid))
            return Unauthorized();

        var createdPost = await _postResource.CreatePost(request, userGuid);
        return Ok(createdPost);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> EditPost(Guid postId, [FromBody] PostUpdateRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userId, out Guid userGuid))
            return Unauthorized();

        var updatedPost = await _postResource.EditPost(postId, request, userGuid);
        return Ok(updatedPost);
    }
}