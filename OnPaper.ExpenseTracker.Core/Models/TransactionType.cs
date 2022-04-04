namespace OnPaper.ExpenseTracker.Core.Models;

public class TransactionType : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}