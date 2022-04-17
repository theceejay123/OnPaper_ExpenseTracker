using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Specifications;

public class TransactionWithTypesSpecification : BaseSpecification<Transaction>
{
    public TransactionWithTypesSpecification(int id, int workBookId)
        : base(x => x.WorkBookId == workBookId && x.Id == id)
    {
        AddInclude(x => x.Include(e => e.PaymentType));
        AddInclude(x => x.Include(e => e.Category)); 
        AddInclude(x => x.Include(e => e.TransactionType));
    }

    public TransactionWithTypesSpecification(int workBookId)
        : base(x => x.WorkBookId == workBookId)
    {
        AddInclude(x => x.Include(e => e.PaymentType));
        AddInclude(x => x.Include(e => e.Category)); 
        AddInclude(x => x.Include(e => e.TransactionType));
    }
}