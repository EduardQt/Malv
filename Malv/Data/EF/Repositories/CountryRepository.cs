using Malv.Data.EF.Entity.Country;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CountryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Country>> GetAll()
    {
        return await _dbContext.Countries
            .Include(i => i.Municipalities)
            .ToListAsync();
    }

    public async Task<Municipality> FindMunicipality(int id)
    {
        return await _dbContext.Municipalities
            .SingleOrDefaultAsync(s => s.Id == id);
    }
}