namespace Malv.Models;

public class Ad_Create_Req
{
    public string Title { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// CarAdMod JSON String because this is FromForm so deserialize.
    /// </summary>
    public string? CarAdMod { get; set; }

    public int CategoryId { get; set; }
    public int MunicipalityId { get; set; }
    
    public ICollection<IFormFile> Files { get; set; }
}