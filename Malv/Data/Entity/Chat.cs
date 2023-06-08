using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class Chat
{
    [Key]
    public int Id { get; set; }
    
    public int AdId { get; set; }
    
    public int UserId { get; set; }
    
    public Ad Ad { get; set; }
    
    public MalvUser User { get; set; }
    
    public ICollection<ChatMessage> ChatMessages { get; set; }
}