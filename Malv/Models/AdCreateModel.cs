namespace Malv.Models;

public class AdCreateModel
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<IFormFile> Files { get; set; }
}