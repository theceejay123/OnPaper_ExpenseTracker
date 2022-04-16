using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface IPaymentTypeService
{
    Task<PaymentType?> CreatePaymentTypeAsync(PaymentType paymentType);
    Task<IReadOnlyList<PaymentType>> ListPaymentTypesAsync();
    Task<PaymentType?> GetPaymentTypeByIdAsync(int id);
}