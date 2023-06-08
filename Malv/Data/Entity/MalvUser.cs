using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class MalvUser
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public bool MailVerified { get; set; }

    public string FullName => FirstName + " " + LastName;
    public ICollection<Ad> Ads { get; set; }
    public UserData UserData { get; set; }
    public ICollection<ChatMessage> ChatMessages { get; set; }
    public ICollection<MailToken> MailTokens { get; set; }
    public ICollection<AdWatch> AdWatches { get; set; }
}