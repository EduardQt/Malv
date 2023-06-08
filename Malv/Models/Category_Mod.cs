namespace Malv.Models;

public class Category_Mod
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Selectable { get; set; }
    public CategoryType_Mod Type { get; set; }
    public IList<Category_Mod> Children { get; set; }
}