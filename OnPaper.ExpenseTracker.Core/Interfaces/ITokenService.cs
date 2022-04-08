using OnPaper.ExpenseTracker.Core.Models.Identity;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}