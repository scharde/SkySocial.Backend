using API.PostVote;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[ApiController]
[Route("api/post-vote")]
[Authorize]
public class PostVoteController(IPostVoteResource postVoteResource) : BaseController
{
    private readonly IPostVoteResource _postVoteResource = postVoteResource;

    [HttpPost]
    public async Task<ActionResult<PostVoteResource>> Vote([FromBody] UserVoteRequest request)
    {
        if (CurrentUserId == null)
            return Unauthorized();

        var result = await _postVoteResource.Post(request.PostId, CurrentUserId.Value, request.Vote);
        return Ok(result);
    }
}