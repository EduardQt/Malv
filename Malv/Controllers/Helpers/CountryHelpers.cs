using Malv.Data.EF.Entity.Country;
using Malv.Models;

namespace Malv.Controllers.Helpers;

public static class CountryHelpers
{
    public static Country_Mod MapCountry(this Country country)
    {
        return new Country_Mod()
        {
            Id = country.Id,
            Name = country.Name,
            Municipalities = country.Municipalities.MapMunicipalities()
        };
    }

    public static Municipality_Mod MapMunicipality(this Municipality municipality)
    {
        return new Municipality_Mod()
        {
            Id = municipality.Id,
            Name = municipality.Name
        };
    }

    public static ICollection<Municipality_Mod> MapMunicipalities(this ICollection<Municipality> municipalities) =>
        municipalities.Select(s => s.MapMunicipality()).ToList();
}