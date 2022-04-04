namespace OnPaper.ExpenseTracker.Core.Models;

public class WorkBook : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<Transaction> Transactions { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateUser { get; set; }
    public DateTime UpdateDate { get; set; }
    public string UpdateUser { get; set; }
}