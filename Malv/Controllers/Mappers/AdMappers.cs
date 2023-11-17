using Malv.Data.EF.Entity;
using Malv.Models;

namespace Malv.Controllers.Mappers;

public static class AdMappers
{
    public static ICollection<Ad_Mod> MapAds(this IEnumerable<Ad> ad) =>
        ad.Select(s => s.MapAd()).ToList();

    public static Ad_Mod MapAd(this Ad ad)
    {
        var adMod = new Ad_Mod()
        {
            Id = ad.Id,
            Title = ad.Title,
            Description = ad.Description,
            Author = ad.User.FullName,
            Images = ad.AdImages.Select(s => s.ImageName).ToList(),
            Price = ad.Price
        };

        if (ad.CarAd != null) adMod.CarAd = ad.CarAd.MapCarAd();
        
        return adMod;
    }

    public static CarAd_Mod MapCarAd(this CarAd carAd)
    {
        return new CarAd_Mod()
        {
            Age = carAd.Age,
            Mileage = carAd.Mileage,
            TransmissionType = (TransmissionType_Mod)carAd.TransmissionType,
            FuelType = (FuelType_Mod)carAd.FuelType,
            CarType = (CarType_Mod)carAd.CarType,
            AllWheelDrive = carAd.AllWheelDrive,
            HorsePower = carAd.HorsePower
        };
    }
}