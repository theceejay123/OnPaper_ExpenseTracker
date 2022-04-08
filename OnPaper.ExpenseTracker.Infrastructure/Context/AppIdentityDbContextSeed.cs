using Microsoft.AspNetCore.Identity;
using OnPaper.ExpenseTracker.Core.Models.Identity;

namespace OnPaper.ExpenseTracker.Infrastructure.Context;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if (userManager.Users.Any())
        {
            return;
        }

        AppUser user = new AppUser()
        {
            DisplayName = "Admin",
            Email = "admin@test.com",
            UserName = "admin@test.com",
            Address = new Address
            {
                FirstName = "Test",
                MiddleName = "Admin",
                LastName = "User",
                Street = "123 TestStreet",
                City = "Winnipeg",
                Province = "MB",
                PostalCode = "X0C2G1"
            }
        };

        await userManager.CreateAsync(user, "Pa$$w0rd");
    }
}