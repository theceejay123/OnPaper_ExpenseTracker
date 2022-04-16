using System.Linq.Expressions;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Specifications;

public class WorkbookWithTransactionsSpecification : BaseSpecification<WorkBook>
{
    public WorkbookWithTransactionsSpecification(string appUserId)
        : base(x => x.AppUserId == appUserId)
    {
        AddInclude(x => x.Transactions);
        AddOrderByDesc(x => x.CreateDate);
    }

    public WorkbookWithTransactionsSpecification(int id, string appUserId)
        : base(x => x.AppUserId == appUserId && x.Id == id)
    {
        AddInclude(x => x.Transactions);
        AddOrderByDesc(x => x.CreateDate);
    }
}