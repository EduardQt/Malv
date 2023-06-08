using Malv.Data.EF;
using Malv.Data.EF.Entity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IList<Malv.Data.EF.Entity.Ad>> Search(string queryString, int? municipalityId, int? categoryId)
    {
        var query = _applicationDbContext.Ads
            .Include(i => i.User)
            .Include(i => i.AdImages)
            .Where(w => w.Title.Contains(queryString));

        if (municipalityId.HasValue)
            query = query.Where(w => w.MunicipalityId == municipalityId);

        if (categoryId.HasValue)
            query = query.Where(w => w.AdCategories.Any(a => a.CategoryId == categoryId));
        
        return await query.ToListAsync();
    }
}