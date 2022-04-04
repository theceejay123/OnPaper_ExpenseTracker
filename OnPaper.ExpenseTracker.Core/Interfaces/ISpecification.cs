using System.Linq.Expressions;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    IList<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>> OrderByAsc { get; }
    Expression<Func<T, object>> OrderByDesc { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}