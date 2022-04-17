using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Specifications;

public class WorkbookWithTransactionsSpecification : BaseSpecification<WorkBook>
{
    public WorkbookWithTransactionsSpecification(string appUserId)
        : base(x => x.AppUserId == appUserId)
    {
        AddOrderByDesc(x => x.UpdateDate);
    }

    public WorkbookWithTransactionsSpecification(int id, string appUserId)
        : base(x => x.AppUserId == appUserId && x.Id == id)
    {
        AddOrderByDesc(x => x.UpdateDate);
    }
}