using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Evaluators;

public class SpecificationEvaluator<T> where T : BaseModel
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        IQueryable<T> query = inputQuery;
        if (specification.Criteria != null) query = query.Where(specification.Criteria);
        if (specification.OrderByAsc != null) query = query.OrderBy(specification.OrderByAsc);
        if (specification.OrderByDesc != null) query = query.OrderByDescending(specification.OrderByDesc);
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        query = specification.Includes.Aggregate(
            query,
            (currentModel, IncludeExpression) => currentModel.Include(IncludeExpression)
        );
        return query;
    }
}