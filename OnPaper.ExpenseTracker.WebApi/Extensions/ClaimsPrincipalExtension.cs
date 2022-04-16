using System.Security.Claims;

namespace OnPaper.ExpenseTracker.WebApi.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string GetUserIdFromClaimsPrincipal(this ClaimsPrincipal userClaims)
    {
        return userClaims?.FindFirstValue("user_id");
    }
}