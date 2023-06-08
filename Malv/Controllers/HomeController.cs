using Malv.Controllers.Helpers;
using Malv.Controllers.Mappers;
using Malv.Data.EF.Entity;
using Malv.Data.EF.Repositories;
using Malv.Data.Repository;
using Malv.Models.HomeController;
using Malv.Services.Ad;
using Microsoft.AspNetCore.Mvc;

namespace Malv.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAdRepository _adRepository;
    private readonly CategoryService _categoryService;

    public HomeController(ILogger<HomeController> logger, IAdRepository adRepository, CategoryService categoryService)
    {
        _logger = logger;
        _adRepository = adRepository;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> BaseCategories()
    {
        var allCategories = await _categoryService.GetAllAsync();
        IList<Category> rootCategories = allCategories.Where(x => x.ParentId == null).ToList();
        foreach (var category in rootCategories)
        {
            category.Children = null;
        }

        return Ok(rootCategories.MapCategories());
    }

    [HttpGet]
    public async Task<IActionResult> LastAds()
    {
        var ads = await _adRepository.Last(10);
        foreach (var ad in ads)
        {
            ad.AdImages = ad.AdImages.Take(1).ToList();
        }

        return Ok(new Home_LastAds_Res()
        {
            Ads = ads.MapAds()
        });
    }
}