namespace Malv.Models.SearchController;

public class Search_Search_Res
{
    public ICollection<Ad_Mod> Ads { get; set; }
    public ICollection<Category_Mod> Categories { get; set; }
    public Category_Mod RootCategory { get; set; }
}