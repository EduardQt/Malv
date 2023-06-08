using Malv.Data.EF.Entity;

namespace Malv.Data.Repository;

public interface ICategoryRepository
{
    Task<IList<Category>> GetAll();

    Task<Category> Find(int id);
}