using Microsoft.IdentityModel.Tokens;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface IGenerateTokenKeyService
{
    ECDsaSecurityKey GetGeneratedECDsaSecurityKey();
}