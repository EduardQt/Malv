using Malv.Data.EF;
using Malv.Data.EF.Entity;
using Malv.Models.SearchController;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Malv.Services.Ad;

public class AdService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _applicationDbContext;

    public AdService(IConfiguration configuration, ApplicationDbContext applicationDbContext)
    {
        _configuration = configuration;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IList<AdImage>> SaveImages(IEnumerable<IFormFile> formFiles, string username)
    {
        IList<AdImage> images = new List<AdImage>();
        foreach (IFormFile formFile in formFiles)
        {
            FileInfo fileInfo = new FileInfo(formFile.FileName);
            string userDirectory = Path.Join(_configuration.GetSection("Static").GetValue<string>("Images"), username);
            if (!Directory.Exists(userDirectory))
                Directory.CreateDirectory(userDirectory);

            string file = Path.Join(userDirectory, fileInfo.Name);
            FileStream fileStream = File.OpenWrite(file);
            await formFile.CopyToAsync(fileStream);

            images.Add(new AdImage
            {
                ImageName = formFile.FileName
            });
        }

        return images;
    }

    public async Task<IList<Malv.Data.EF.Entity.Ad>> Search(string queryString, int? municipalityId, int? categoryId, Search_CarFilter_Req carFilter)
    {
        var query = _applicationDbContext.Ads
            .Include(i => i.User)
            .Include(i => i.AdImages)
            .Where(w => w.Title.Contains(queryString));

        if (municipalityId.HasValue)
            query = query.Where(w => w.MunicipalityId == municipalityId);

        if (categoryId.HasValue)
            query = query.Where(w => w.AdCategories.Any(a => a.CategoryId == categoryId));
        
        FilterCarAds(ref query, carFilter);

        return await query.ToListAsync();
    }

    private static void FilterCarAds(ref IQueryable<Malv.Data.EF.Entity.Ad> query, Search_CarFilter_Req carFilter)
    {
        bool notAllNull = carFilter.GetType().GetProperties()
            .Where(pi => pi.PropertyType == typeof(string))
            .Select(pi => (string)pi.GetValue(carFilter))
            .Any(string.IsNullOrEmpty);
        if (carFilter.CarType.HasValue)
            query = query.Where(w => w.CarAd.CarType == (CarType)carFilter.CarType);

        if (carFilter.MinAge.HasValue)
            query = query.Where(w => w.CarAd.Age >= carFilter.MinAge);

        if (carFilter.MaxAge.HasValue)
            query = query.Where(w => w.CarAd.Age <= carFilter.MaxAge);

        if (carFilter.MinMileage.HasValue)
            query = query.Where(w => w.CarAd.Mileage >= carFilter.MinMileage);

        if (carFilter.MaxMileage.HasValue)
            query = query.Where(w => w.CarAd.Mileage <= carFilter.MaxMileage);

        if (carFilter.Transmission.HasValue)
            query = query.Where(w => w.CarAd.TransmissionType == (TransmissionType)carFilter.Transmission);

        if (carFilter.Fuel.HasValue)
            query = query.Where(w => w.CarAd.FuelType == (FuelType)carFilter.Fuel);

        if (carFilter.CarType.HasValue)
            query = query.Where(w => w.CarAd.CarType == (CarType)carFilter.CarType);

        if (carFilter.DriveTrain.HasValue)
            query = query.Where(w => w.CarAd.AllWheelDrive == carFilter.DriveTrain);
    }
}