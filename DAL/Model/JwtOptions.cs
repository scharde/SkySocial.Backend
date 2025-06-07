namespace DAL.Model;

public class JwtOptions
{
    public const string JwtOptionsKey = "JwtOptions";
    
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationTimeInMinutes { get; set; }  
}

public record TokenResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiryTime { get; set; }
}