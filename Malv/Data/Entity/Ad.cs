using System.ComponentModel.DataAnnotations;
using Malv.Data.EF.Entity.Country;

namespace Malv.Data.EF.Entity;

public class Ad
{
    [Key]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public int UserId { get; set; }
    public int MunicipalityId { get; set; }
    
    public MalvUser User { get; set; }
    public ICollection<AdImage> AdImages { get; set; }
    
    public CarAd CarAd { get; set; }
    public Municipality Municipality { get; set; }
    public ICollection<AdWatch> AdWatches { get; set; }
    public ICollection<AdCategory> AdCategories { get; set; }
}