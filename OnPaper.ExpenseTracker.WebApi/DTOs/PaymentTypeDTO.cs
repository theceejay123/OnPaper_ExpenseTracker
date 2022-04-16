namespace OnPaper.ExpenseTracker.WebApi.DTOs;

public class PaymentTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}