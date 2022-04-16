namespace OnPaper.ExpenseTracker.WebApi.DTOs;

public class AppUserDTO
{
    public string AppUserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string Token { get; set; } = string.Empty;
}