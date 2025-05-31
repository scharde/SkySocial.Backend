using API.Comments;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController(ICommentsResource commentsResource) : BaseController
{
    private readonly ICommentsResource _commentsResource = commentsResource;

    [HttpGet]
    public async Task<IActionResult> GetComments(Guid postId, int page = 1, int pageSize = 20)
    {
        var result = await _commentsResource.Get(postId, page, pageSize);
        return Ok(result);   
    }
    
    [HttpGet("{parentCommentId}/replies")]
    public async Task<IActionResult> GetReplies(Guid parentCommentId, int page = 1, int pageSize = 10)
    {
        var result = await _commentsResource.GetReplies(parentCommentId, page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CommentCreateRequest request)
    {
        if (CurrentUserId == null)
            return Unauthorized();
        
        var result = await _commentsResource.CreateComment(request, CurrentUserId.Value);

        return Ok(result);
    }
}