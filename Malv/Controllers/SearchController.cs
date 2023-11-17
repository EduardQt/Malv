using Malv.Controllers.Helpers;
using Malv.Controllers.Mappers;
using Malv.Data.EF.Entity;
using Malv.Models.SearchController;
using Malv.Services.Ad;
using Microsoft.AspNetCore.Mvc;

namespace Malv.Controllers;

public class SearchController : Controller
{
    private readonly AdService _adService;
    private readonly CategoryService _categoryService;

    public SearchController(AdService adService, CategoryService categoryService)
    {
        _adService = adService;
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody] Search_Search_Req req)
    {
        var ads = await _adService.Search(req.Query, req.MunicipalityId, req.CategoryId, req.CarFilter);

        ICollection<Category> categories;
        Category rootCategory = null;
        if (req.CategoryId.HasValue)
        {
            Category category = await _categoryService.FindCategory(req.CategoryId.Value);
            categories = category.Children;
            rootCategory = _categoryService.FindRootCategory(category);
        }
        else
        {
            categories = await _categoryService.GetAllAsync();
            categories = categories.Where(w => w.ParentId == null).ToList();
        }

        foreach (var ad in ads)
        {
            ad.AdImages = ad.AdImages.Take(1).ToList();
        }

        var ret = new Search_Search_Res
        {
            Ads = ads.MapAds(),
            Categories = categories.MapCategories(),
        };
        if (rootCategory != null) ret.RootCategory = rootCategory.MapRootCategory();
        return Ok(ret);
    }
}