namespace Malv.Data.EF.Entity.Country;

public class Municipality
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<Ad> Ads { get; set; }
}