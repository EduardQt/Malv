using Malv.Data.EF.Entity;

namespace Malv.Data.Repository;

public interface IAdRepository
{
    Task CreateAdd(Ad ad);
    
    Task Update(Ad ad);

    Task<IList<Ad>> FindAdsByCategories(IList<int> categoryIds);

    Task<Ad> Find(int id);

    Task<IList<Ad>> Last(int count);

    Task<IList<int>> FindAdIdsByUserId(int userId);
}