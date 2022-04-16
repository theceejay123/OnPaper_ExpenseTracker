using OnPaper.ExpenseTracker.Core.Models.Identity;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface IAccountService
{
    Task<AppUser> LoginAsync(Login login);
    Task<AppUser> RegisterAsync(Register register);
}