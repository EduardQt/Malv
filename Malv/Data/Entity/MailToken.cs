using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class MailToken
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public string Token { get; set; }

    public MailTokenType TokenType { get; set; }
    
    public MalvUser User { get; set; }
    
    public enum MailTokenType
    {
        VerifyMail
    }
}