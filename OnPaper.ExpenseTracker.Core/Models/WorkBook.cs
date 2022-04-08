using System.ComponentModel.DataAnnotations;

namespace OnPaper.ExpenseTracker.Core.Models;

public class WorkBook : BaseModel
{
    [Required] public string AppUserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}