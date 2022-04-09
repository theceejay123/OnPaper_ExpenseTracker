using System.ComponentModel.DataAnnotations;

namespace OnPaper.ExpenseTracker.Core.Models;

public class Transaction : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Notes { get; set; } = string.Empty;
    public TransactionType TransactionType { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public PaymentType PaymentType { get; set; } = null!;
    [Required] public int WorkBookId { get; set; }
    public WorkBook WorkBook { get; set; } = null!;
    public int TransactionTypeId { get; set; }
    public int CategoryId { get; set; }
    public int PaymentTypeId { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateUser { get; set; } = string.Empty;
    public DateTime UpdateDate { get; set; }
    public string UpdateUser { get; set; } = string.Empty;
}