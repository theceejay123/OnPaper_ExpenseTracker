namespace OnPaper.ExpenseTracker.Core.Models;

public class PaymentType : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}