using System.Security.Claims;
using API.Post;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class PostController(IPostResource postResource) : BaseController
{
    private readonly IPostResource _postResource = postResource;

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateRequest request)
    {
        if (CurrentUserId == null)
            return Unauthorized();

        var createdPost = await _postResource.CreatePost(request, CurrentUserId.Value);
        return Ok(createdPost);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> EditPost(Guid postId, [FromBody] PostUpdateRequest request)
    {
        if (CurrentUserId == null)
            return Unauthorized();

        var updatedPost = await _postResource.EditPost(postId, request, CurrentUserId.Value);
        return Ok(updatedPost);
    }
}