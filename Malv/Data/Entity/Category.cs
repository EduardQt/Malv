using System.ComponentModel.DataAnnotations.Schema;

namespace Malv.Data.EF.Entity;

public class Category
{
    public int Id { get; set; }
    
    public int? ParentId { get; set; }
    
    public string Name { get; set; }
    
    public Category Parent { get; set; }
    
    public List<Category> Children { get; set; }
    
    public CategoryType Type { get; set; }
    public ICollection<AdCategory> AdCategories { get; set; }
    [NotMapped]
    public int Level { get; set; }
    public enum CategoryType : byte
    {
        None,
        Cars,
        Jobs,
        ForHome,
        Housing,
        Personal,
        Electronics,
        Hobbies,
        WorkOperations
    }
}