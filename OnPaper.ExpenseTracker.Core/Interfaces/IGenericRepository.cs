using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetWithSpecificationAsync(ISpecification<T> specification);
    Task<IReadOnlyList<T>> ListAsync();
    Task<IReadOnlyList<T>> ListWithSpecificationAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
    void Create(T model);
    void Update(T model);
    void Delete(T model);
}