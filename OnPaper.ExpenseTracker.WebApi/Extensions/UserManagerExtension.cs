using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Core.Models.Identity;

namespace OnPaper.ExpenseTracker.WebApi.Extensions;

public static class UserManagerExtension
{
    public static async Task<AppUser> FindByClaimsPrincipalWithAddressAsync(this UserManager<AppUser> input,
        ClaimsPrincipal user)
    {
        var appUserId = user?.Claims?.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => appUserId.Equals(x.Id));
    }
    
    public static async Task<AppUser> FindByClaimsPrincipalAsync(this UserManager<AppUser> input,
        ClaimsPrincipal user)
    {
        var appUserId = user?.Claims?.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return await input.Users.SingleOrDefaultAsync(x => appUserId.Equals(x.Id));
    }
}