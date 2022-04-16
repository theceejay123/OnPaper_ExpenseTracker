using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface ITransactionTypeService
{
    Task<TransactionType?> CreateTransactionTypeAsync(TransactionType? transactionType);
    Task<IReadOnlyList<TransactionType>> ListTransactionTypesAsync();
    Task<TransactionType?> GetTransactionTypeByIdAsync(int id);
}