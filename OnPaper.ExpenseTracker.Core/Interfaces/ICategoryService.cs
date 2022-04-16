using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface ICategoryService
{
    Task<Category?> CreateCategoryAsync(Category category);
    Task<IReadOnlyList<Category>> ListCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
}