using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface ITransactionService
{
    Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    Task<Transaction> DeleteTransactionAsync(int id, int workBookId);
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
    Task<IReadOnlyList<Transaction>> ListTransactionsAsync(int workBookId);
    Task<Transaction> GetTransactionByIdAsync(int id, int workBookId);
}