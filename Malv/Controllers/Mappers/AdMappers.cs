using Malv.Data.EF.Entity;
using Malv.Models;

namespace Malv.Controllers.Mappers;

public static class AdMappers
{
    public static ICollection<Ad_Mod> MapAds(this IEnumerable<Ad> ad) =>
        ad.Select(s => s.MapAd()).ToList();

    public static Ad_Mod MapAd(this Ad ad)
    {
        return new Ad_Mod()
        {
            Id = ad.Id,
            Title = ad.Title,
            Description = ad.Description,
            Author = ad.User.FullName,
            Images = ad.AdImages.Select(s => s.ImageName).ToList()
        };
    }
}