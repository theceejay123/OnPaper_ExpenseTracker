using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.Core.Specifications;

namespace OnPaper.ExpenseTracker.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        var spec = new TransactionWithTypesSpecification(transaction.Id, transaction.WorkBookId);
        var transactionToUpdate = await _unitOfWork.Repository<Transaction>().GetWithSpecificationAsync(spec);
        if (transactionToUpdate == null)
        {
            return null;
        }

        transactionToUpdate.Name = transaction.Name;
        transactionToUpdate.Notes = transaction.Notes;
        transactionToUpdate.Amount = transaction.Amount;
        transactionToUpdate.CategoryId = transaction.CategoryId;
        transactionToUpdate.PaymentTypeId = transaction.PaymentTypeId;
        transactionToUpdate.TransactionTypeId = transaction.TransactionTypeId;
        transactionToUpdate.UpdateDate = DateTime.UtcNow;
        _unitOfWork.Repository<Transaction>().Update(transactionToUpdate);
        await _unitOfWork.CompleteAsync();
        return await GetTransactionByIdAsync(transactionToUpdate.Id, transactionToUpdate.WorkBookId);
    }

    public async Task<Transaction> DeleteTransactionAsync(int id, int workBookId)
    {
        var spec = new TransactionWithTypesSpecification(id, workBookId);
        var transactionToDelete = await _unitOfWork.Repository<Transaction>().GetWithSpecificationAsync(spec);
        if (transactionToDelete == null)
        {
            return null;
        }
        _unitOfWork.Repository<Transaction>().Delete(transactionToDelete);
        await _unitOfWork.CompleteAsync();
        return transactionToDelete;
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        _unitOfWork.Repository<Transaction>().Create(transaction);
        var result = await _unitOfWork.CompleteAsync();
        return result <= 0 ? null : await GetTransactionByIdAsync(transaction.Id, transaction.WorkBookId);
    }

    public async Task<IReadOnlyList<Transaction>> ListTransactionsAsync(int workBookId)
    {
        var spec = new TransactionWithTypesSpecification(workBookId);
        return await _unitOfWork.Repository<Transaction>().ListWithSpecificationAsync(spec);
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id, int workBookId)
    {
        var spec = new TransactionWithTypesSpecification(id, workBookId);
        return await _unitOfWork.Repository<Transaction>().GetWithSpecificationAsync(spec);
    }
}