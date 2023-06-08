using Malv.Data.EF.Entity.Country;

namespace Malv.Data.Repository;

public interface ICountryRepository
{
    Task<IList<Country>> GetAll();

    Task<Municipality> FindMunicipality(int id);
}