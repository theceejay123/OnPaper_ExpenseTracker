using System.Collections;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.Infrastructure.Context;

namespace OnPaper.ExpenseTracker.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly TransactionContext _context;
    private Hashtable _repositories = null!;

    public UnitOfWork(TransactionContext context)
    {
        _context = context;
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public IGenericRepository<T>? Repository<T>() where T : BaseModel
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repoInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repoInstance);
        }

        return (IGenericRepository<T>?) _repositories[type];
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}