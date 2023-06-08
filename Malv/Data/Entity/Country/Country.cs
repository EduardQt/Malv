namespace Malv.Data.EF.Entity.Country;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Municipality> Municipalities { get; set; }
}