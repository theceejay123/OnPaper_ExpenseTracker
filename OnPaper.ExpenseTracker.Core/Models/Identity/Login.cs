namespace OnPaper.ExpenseTracker.Core.Models.Identity;

public abstract class Login
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}