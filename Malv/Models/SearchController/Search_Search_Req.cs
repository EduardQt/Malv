namespace Malv.Models.SearchController;

public class Search_Search_Req
{
    public string Query { get; set; }
    public int? MunicipalityId { get; set; }
    
    public int? CategoryId { get; set; }

    public Search_Order_Mod Order { get; set; }

    public Search_CarFilter_Req CarFilter { get; set; }
}