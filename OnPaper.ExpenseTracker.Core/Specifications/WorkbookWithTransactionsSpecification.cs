using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Specifications;

public class WorkbookWithTransactionsSpecification : BaseSpecification<WorkBook>
{
    public WorkbookWithTransactionsSpecification(string appUserId)
        : base(x => x.AppUserId == appUserId)
    {
        AddInclude(x => x.Include(e => e.Transactions).ThenInclude(e => e.Category));
        AddInclude(x => x.Include(e => e.Transactions).ThenInclude(e => e.PaymentType));
        AddInclude(x => x.Include(e => e.Transactions).ThenInclude(e => e.TransactionType));
        AddOrderByDesc(x => x.CreateDate);
    }

    public WorkbookWithTransactionsSpecification(int id, string appUserId)
        : base(x => x.AppUserId == appUserId && x.Id == id)
    {
        AddInclude(x => x.Include(e => e.Transactions).ThenInclude(e => e.Category));
        AddInclude(x => x.Include(e => e.Transactions).ThenInclude(e => e.PaymentType));
        AddInclude(x => x.Include(e => e.Transactions).ThenInclude(e => e.TransactionType));
        AddOrderByDesc(x => x.CreateDate);
    }
}