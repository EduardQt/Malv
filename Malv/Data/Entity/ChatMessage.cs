using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class ChatMessage
{
    [Key]
    public int Id { get; set; }
    
    public string Content { get; set; }
    
    public int ChatId { get; set; }
    
    public int UserId { get; set; }
    
    public DateTime SentDate { get; set; }
    
    public bool IsRead { get; set; }
    
    public Chat Chat { get; set; }
    public MalvUser User { get; set; }
}