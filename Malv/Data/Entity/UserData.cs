using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class UserData
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? RefreshToken { get; set; }
    
    public MalvUser User { get; set; }
}