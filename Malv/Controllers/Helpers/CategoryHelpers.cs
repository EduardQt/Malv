using Malv.Data.EF.Entity;
using Malv.Models;

namespace Malv.Controllers.Helpers;

public static class CategoryHelpers
{

    public static IList<Category_Mod> MapCategories(this ICollection<Category> categories, int? levels = null, int currentLevel = 0)
    {
        if (categories == null || categories.Count == 0 || (levels.HasValue && levels == currentLevel)) return new List<Category_Mod>();

        IList<Category_Mod> categoryModels = new List<Category_Mod>();
        foreach (Category category in categories)
        {
            Category_Mod categoryModel = new Category_Mod();
            if (category.Children == null || category.Children.Count == 0 || category.Name == "Tjera")
            {
                categoryModel.Selectable = true;
            }

            categoryModel.Id = category.Id;
            categoryModel.Title = category.Name;
            categoryModel.Type = (CategoryType_Mod)category.Type;
            categoryModel.Children = MapCategories(category.Children, levels, currentLevel + 1);
            categoryModels.Add(categoryModel);
        }

        return categoryModels;
    }
    
    public static Category_Mod MapRootCategory(this Category rootCategory)
    {
        Category_Mod categoryModel = new Category_Mod();
        if (rootCategory.Children == null || rootCategory.Children.Count == 0 || rootCategory.Name == "Tjera")
        {
            categoryModel.Selectable = true;
        }

        categoryModel.Id = rootCategory.Id;
        categoryModel.Title = rootCategory.Name;
        categoryModel.Type = (CategoryType_Mod)rootCategory.Type;
        categoryModel.Children = MapCategories(rootCategory.Children, 1);

        return categoryModel;
    }
}