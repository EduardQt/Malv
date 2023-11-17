namespace Malv.Models;

public class Ad_Mod
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public IList<string> Images { get; set; }
    public decimal Price { get; set; }
    public CarAd_Mod CarAd { get; set; }
}