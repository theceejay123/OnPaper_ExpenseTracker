using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using OnPaper.ExpenseTracker.Core.Interfaces;

namespace OnPaper.ExpenseTracker.Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; } = null!;
    public IList<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();
    public Expression<Func<T, object>> OrderByAsc { get; private set; } = null!;
    public Expression<Func<T, object>> OrderByDesc { get; private set; } = null!;
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddOrderByAsc(Expression<Func<T, object>> orderByAscExpression)
    {
        OrderByAsc = orderByAscExpression;
    }
    
    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }

    protected void AddPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}