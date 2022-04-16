using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category?> CreateCategoryAsync(Category category)
    {
        _unitOfWork.Repository<Category>().Create(category);
        var result = await _unitOfWork.CompleteAsync();
        return result <= 0 ? null : category;
    }

    public async Task<IReadOnlyList<Category>> ListCategoriesAsync()
    {
        return await _unitOfWork.Repository<Category>().ListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _unitOfWork.Repository<Category>().GetByIdAsync(id);
    }
}