using System.ComponentModel.DataAnnotations;

namespace OnPaper.ExpenseTracker.Core.Models;

public class WorkBook : BaseModel
{
    [Required] public string AppUserId { get; set; } = Guid.Empty.ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Transaction> Transactions { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}