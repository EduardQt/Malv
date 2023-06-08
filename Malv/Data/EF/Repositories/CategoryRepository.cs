using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Category>> GetAll()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> Find(int id)
    {
        return await _dbContext.Categories
            .Include(i => i.Parent)
            .SingleOrDefaultAsync(s => s.Id == id);
    }
}