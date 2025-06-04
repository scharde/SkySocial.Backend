using DAL.Entity;

namespace DAL.Model;

public class UserVoteRequest
{
    public Guid PostId { get; set; }
    public int Vote { get; set; }
}

public class UserVoteResponse
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public VoteType Type { get; set; }
}