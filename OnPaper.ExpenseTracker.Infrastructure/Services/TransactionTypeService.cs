using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Services;

public class TransactionTypeService : ITransactionTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionType?> CreateTransactionTypeAsync(TransactionType? transactionType)
    {
        if (transactionType == null)
        {
            return null;
        } 
        _unitOfWork.Repository<TransactionType>().Create(transactionType);
        var result = await _unitOfWork.CompleteAsync();
        return result <= 0 ? null : transactionType;
    }

    public async Task<IReadOnlyList<TransactionType>> ListTransactionTypesAsync()
    {
        return await _unitOfWork.Repository<TransactionType>().ListAsync();
    }

    public async Task<TransactionType?> GetTransactionTypeByIdAsync(int id)
    { 
        return await _unitOfWork.Repository<TransactionType>().GetByIdAsync(id);
    }
}