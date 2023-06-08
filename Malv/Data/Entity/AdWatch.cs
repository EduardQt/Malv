using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class AdWatch
{
    [Key]
    public int Id { get; set; }
    
    public int AdId { get; set; }
    
    public int UserId { get; set; }
    
    public MalvUser User { get; set; }
    
    public Ad Ad { get; set; }
}