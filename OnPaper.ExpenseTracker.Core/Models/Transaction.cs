namespace OnPaper.ExpenseTracker.Core.Models;

public class Transaction : BaseModel
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public TransactionType TransactionType { get; set; }
    public Category Category { get; set; }
    public PaymentType PaymentType { get; set; }
    public int TransactionTypeId { get; set; }
    public int CategoryId { get; set; }
    public int PaymentTypeId { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateUser { get; set; }
    public DateTime UpdateDate { get; set; }
    public string UpdateUser { get; set; }
}