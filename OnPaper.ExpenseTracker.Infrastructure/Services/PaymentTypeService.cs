using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Services;

public class PaymentTypeService : IPaymentTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentType?> CreatePaymentTypeAsync(PaymentType paymentType)
    {
        _unitOfWork.Repository<PaymentType>().Create(paymentType);
        var result = await _unitOfWork.CompleteAsync();
        return result <= 0 ? null : paymentType;
    }

    public async Task<IReadOnlyList<PaymentType>> ListPaymentTypesAsync()
    {
        return await _unitOfWork.Repository<PaymentType>().ListAsync();
    }

    public async Task<PaymentType?> GetPaymentTypeByIdAsync(int id)
    {
        return await _unitOfWork.Repository<PaymentType>().GetByIdAsync(id);
    }
}