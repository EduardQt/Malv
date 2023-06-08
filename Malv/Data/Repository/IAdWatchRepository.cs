using Malv.Data.EF.Entity;

namespace Malv.Data.Repository;

public interface IAdWatchRepository
{
    Task<IList<AdWatch>> FindAdWatchesWithAdByUserId(int userId);
    
    Task<IList<AdWatch>> FindAdWatchesByUserId(int userId);

    Task AddAdWatch(AdWatch adWatch);
    
    Task RemoveAdWatch(AdWatch adWatch);

    Task RemoveFirstAdWatch(int userId);
}