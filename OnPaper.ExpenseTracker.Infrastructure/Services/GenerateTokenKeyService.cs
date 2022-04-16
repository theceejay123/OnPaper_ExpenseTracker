using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using OnPaper.ExpenseTracker.Core.Interfaces;

namespace OnPaper.ExpenseTracker.Infrastructure.Services;

public class GenerateTokenKeyService : IGenerateTokenKeyService
{

    public ECDsaSecurityKey GetGeneratedECDsaSecurityKey()
    {
        return new ECDsaSecurityKey(GenerateECDsa());
    }

    private static ECDsa GenerateECDsa()
    {
        var privateKey = File.ReadAllText("../../_keys/private_key.pem");
        var generatedKey = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        generatedKey.ImportFromPem(privateKey);
        return generatedKey;
    }
}