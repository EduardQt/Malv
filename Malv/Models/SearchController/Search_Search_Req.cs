namespace Malv.Models.SearchController;

public class Search_Search_Req
{
    public string Query { get; set; }
    public int? MunicipalityId { get; set; }
    
    public int? CategoryId { get; set; }
}