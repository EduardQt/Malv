using Malv.Controllers.Exceptions;
using Malv.Controllers.Helpers;
using Malv.Controllers.Mappers;
using Malv.Controllers.Validators;
using Malv.Data.EF.Entity;
using Malv.Data.EF.Entity.Country;
using Malv.Data.Repository;
using Malv.Filters;
using Malv.Models;
using Malv.Services.Ad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Malv.Controllers;

public class AdController : Controller
{
    private readonly IAdRepository _adRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly CategoryService _categoryService;
    private readonly AdService _adService;
    private readonly IAdWatchRepository _adWatchRepository;

    public AdController(IAdRepository adRepository, ICategoryRepository categoryRepository, AdService adService,
        ICountryRepository countryRepository, CategoryService categoryService, IAdWatchRepository adWatchRepository)
    {
        _adRepository = adRepository;
        _categoryRepository = categoryRepository;
        _adService = adService;
        _countryRepository = countryRepository;
        _categoryService = categoryService;
        _adWatchRepository = adWatchRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        Ad ad = await _adRepository.Find(id);
        if (ad == null)
            throw new BusinessMalvException(new Error_Res().AddError("INVALID_AD", "Ad not fount!"));
        return Ok(ad.MapAd());
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> CreateGet()
    {
        IList<Category> categories = await _categoryService.GetAllAsync();
        IList<Country> countries = await _countryRepository.GetAll();
        IList<Category_Mod> mappedCategories = categories.Where(w => w.ParentId == null).ToList().MapCategories();

        return Ok(new Ad_CreateGet_Res()
        {
            Country = countries.Single().MapCountry(),
            Categories = mappedCategories
        });
    }

    [HttpPost]
    [Authorize]
    [Transaction]
    public async Task<IActionResult> Create([FromForm] Ad_Create_Req model)
    {
        Category category = await _categoryService.FindCategory(model.CategoryId);
        Municipality municipality = await _countryRepository.FindMunicipality(model.MunicipalityId);

        Ad ad = new Ad()
        {
            Title = model.Title,
            Description = model.Description,
            UserId = User.GetUserId(),
            MunicipalityId = municipality.Id
        };
        
        IList<AdCategory> adCategories = new List<AdCategory>();
        _categoryService.CreateAdCategories(category, ad, ref adCategories);
        ad.AdCategories = adCategories;
        
        if (category.Type == Category.CategoryType.Cars)
        {
            CarAd_Mod carAdMod = model.CarAdMod.ValidateAdModel();
            ad.CarAd = new CarAd()
            {
                Age = carAdMod.Age,
                CarType = (CarType)carAdMod.CarType,
                FuelType = (FuelType)carAdMod.FuelType,
                Mileage = carAdMod.Mileage,
                TransmissionType = (TransmissionType)carAdMod.TransmissionType
            };
        }

        await _adRepository.CreateAdd(ad);
        IList<AdImage> images = await _adService.SaveImages(model.Files, ad.Id.ToString());
        ad.AdImages = images;
        await _adRepository.Update(ad);

        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Transaction]
    public async Task AddAdWatch([FromBody] Ad_AddAdWatch_Req req)
    {
        var adWatches = await _adWatchRepository.FindAdWatchesByUserId(User.GetUserId());
        var cachedAdWatch = adWatches.SingleOrDefault(s => s.AdId == req.AdId);

        if (adWatches.Count == 10 && cachedAdWatch == null)
        {
            await _adWatchRepository.RemoveFirstAdWatch(User.GetUserId());
        }

        if (cachedAdWatch != null)
        {
            await _adWatchRepository.RemoveAdWatch(cachedAdWatch);
        }

        var newAdWatch = new AdWatch
        {
            UserId = User.GetUserId(),
            AdId = req.AdId
        };

        await _adWatchRepository.AddAdWatch(newAdWatch);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAdWatches()
    {
        var adWatches = await _adWatchRepository.FindAdWatchesWithAdByUserId(User.GetUserId());
        foreach (var adWatch in adWatches)
        {
            var ad = adWatch.Ad;
            ad.AdImages = ad.AdImages.Take(1).ToList();
        }

        return Ok(new Ad_GetAdWatches_Res
        {
            Ads = adWatches.Select(s => s.Ad).Select(s => s.MapAd()).ToList()
        });
    }
}