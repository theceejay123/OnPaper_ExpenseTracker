namespace OnPaper.ExpenseTracker.WebApi.DTOs;

public class TransactionTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}