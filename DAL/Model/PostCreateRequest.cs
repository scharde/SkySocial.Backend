using DAL.Entity;

namespace DAL.Model;

public class PostCreateRequest
{
    public string Content { get; set; }
}

public class PostUpdateRequest
{
    public string Content { get; set; }
}



public class PostResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }

    public AuthorDetail Author { get; set; }
    public int Score { get; set; }
    public int CommentCount { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
    public VoteType UserVote { get; set; }
    public DateTime CreatedDateUtc { get; set; }
}