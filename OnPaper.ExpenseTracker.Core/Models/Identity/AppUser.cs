using Microsoft.AspNetCore.Identity;

namespace OnPaper.ExpenseTracker.Core.Models.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public Address Address { get; set; } = null!;
}