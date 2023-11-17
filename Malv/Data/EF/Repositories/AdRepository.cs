using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class AdRepository : IAdRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AdRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAdd(Ad ad)
    {
        await _dbContext.Ads.AddAsync(ad);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Ad ad)
    {
        _dbContext.Ads.Update(ad);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<Ad>> FindAdsByCategories(IList<int> categoryIds)
    {
        throw new NullReferenceException();
    }

    public async Task<Ad> Find(int id)
    {
        return await _dbContext.Ads
            .Include(i => i.AdImages)
            .Include(i => i.User)
            .Include(i => i.CarAd)
            .SingleOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IList<Ad>> Last(int count)
    {
        return await _dbContext.Ads
            .OrderByDescending(o => o.Id)
            .Include(i => i.AdImages)
            .Include(i => i.User)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IList<int>> FindAdIdsByUserId(int userId)
    {
        return await _dbContext.Ads
            .Where(w => w.UserId == userId)
            .Select(s => s.Id)
            .ToListAsync();
    }
}