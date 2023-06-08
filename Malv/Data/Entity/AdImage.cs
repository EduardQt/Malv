using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class AdImage
{
    [Key]
    public int Id { get; set; }
    
    public string ImageName { get; set; }
    
    public int AdId { get; set; }
    
    public Ad Ad { get; set; }
}