namespace DAL.Model;

public class UserModelResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public string Avatar { get; set; }
}

public class AuthorDetail
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Avatar { get; set; }
    public bool IsFollowing { get; set; }
}