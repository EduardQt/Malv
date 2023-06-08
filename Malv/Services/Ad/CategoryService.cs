using Malv.Data.EF.Entity;
using Malv.Data.Repository;

namespace Malv.Services.Ad;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    private IList<Category> _categories = new List<Category>();
    private int _getAllAccesses = 0;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IList<Category>> GetAllAsync()
    {
        if (++_getAllAccesses > 100 || _categories.Count == 0)
        {
            _categories = await _categoryRepository.GetAll();
            ConstructLevels();
        }

        return _categories;
    }

    public async Task<Category> FindCategory(int categoryId)
    {
        if (_categories.Count == 0)
        {
            _categories = await _categoryRepository.GetAll();
            ConstructLevels();
        }

        return _categories.SingleOrDefault(s => s.Id == categoryId);
    }

    public Category FindRootCategory(Category category)
    {
        if (category.Parent == null) return category;

        return FindRootCategory(category.Parent);
    }
    
    public void CreateAdCategories(Category category, Data.EF.Entity.Ad ad, ref IList<AdCategory> adCategories)
    {
        adCategories.Add(new AdCategory
        {
            Ad = ad,
            Category = category
        });
        
        if (category.Parent == null) return;
        
        CreateAdCategories(category.Parent, ad, ref adCategories);
    }
    
    private void ConstructLevels()
    {
        IList<Category> baseCategories = _categories.Where(w => w.ParentId == null).ToList();
        ConstructLevels(baseCategories, 0);
    }

    private void ConstructLevels(IList<Category> categories, int level)
    {
        foreach (Category category in categories)
        {
            category.Level = level;
            if (category.Type == Category.CategoryType.None &&
                category.Parent != null) category.Type = category.Parent.Type;

            if (category.Children != null && category.Children.Count > 0)
                ConstructLevels(category.Children, level + 1);
        }
    }
}