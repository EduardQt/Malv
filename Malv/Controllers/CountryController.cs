using Malv.Controllers.Helpers;
using Malv.Data.EF.Entity.Country;
using Malv.Data.Repository;
using Malv.Models.CountryController;
using Microsoft.AspNetCore.Mvc;

namespace Malv.Controllers;

public class CountryController : Controller
{
    private readonly ILogger<CountryController> _logger;
    private readonly ICountryRepository _countryRepository;

    private IList<Country> _cached = new List<Country>();

    public CountryController(ICountryRepository countryRepository, ILogger<CountryController> logger)
    {
        _countryRepository = countryRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountry()
    {
        if (_cached.Count == 0)
            _cached = await _countryRepository.GetAll();

        return Ok(new Country_GetCountry_Res
        {
            Country = _cached.Single().MapCountry()
        });
    }
}