using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class AdWatchRepository : IAdWatchRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AdWatchRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<AdWatch>> FindAdWatchesWithAdByUserId(int userId)
    {
        return await _dbContext.AdWatches
            .Include(i => i.Ad).ThenInclude(ti => ti.AdImages)
            .Include(i => i.Ad).ThenInclude(ti => ti.User)
            .Where(w => w.UserId == userId)
            .ToListAsync();
    }

    public async Task<IList<AdWatch>> FindAdWatchesByUserId(int userId)
    {
        return await _dbContext.AdWatches
            .Where(w => w.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAdWatch(AdWatch adWatch)
    {
        await _dbContext.AdWatches.AddAsync(adWatch);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAdWatch(AdWatch adWatch)
    {
        _dbContext.AdWatches.Remove(adWatch);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveFirstAdWatch(int userId)
    {
        var first = await _dbContext.AdWatches.FirstAsync(f => f.UserId == userId);
        _dbContext.AdWatches.Remove(first);
        await _dbContext.SaveChangesAsync();
    }
}