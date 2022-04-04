using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.Infrastructure.Context;
using OnPaper.ExpenseTracker.Infrastructure.Evaluators;

namespace OnPaper.ExpenseTracker.Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly TransactionContext _context;

    public GenericRepository(TransactionContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetWithSpecificationAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListWithSpecificationAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    public void Create(T model)
    {
        _context.Set<T>().Add(model);
    }

    public void Update(T model)
    {
        _context.Set<T>().Update(model);
    }

    public void Delete(T model)
    {
        _context.Set<T>().Remove(model);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }
}