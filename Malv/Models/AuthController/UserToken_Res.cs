namespace Malv.Models.AuthController;

public class UserToken_Res
{
    public string Token { get; set; }
    public string Username { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ValidTo { get; set; }
}