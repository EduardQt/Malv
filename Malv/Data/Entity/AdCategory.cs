using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class AdCategory
{
    [Key]
    public int Id { get; set; }
    public int CategoryId { get; set; }
    
    public int AdId { get; set; }
    
    public Category Category { get; set; }
    
    public Ad Ad { get; set; }
}