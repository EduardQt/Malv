namespace Malv.Models;

public class District_Mod
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Municipality_Mod> Municipalities { get; set; }
}