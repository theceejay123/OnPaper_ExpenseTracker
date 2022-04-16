using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models.Identity;

namespace OnPaper.ExpenseTracker.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly ECDsaSecurityKey _key;

    public TokenService(IConfiguration configuration, IGenerateTokenKeyService tokenKeyService)
    {
        _configuration = configuration;
        _key = tokenKeyService.GetGeneratedECDsaSecurityKey();
    }

    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>()
        {
            new("email", user.Email),
            new("preferred_name", user.DisplayName),
            new("user_id", user.Id)
        };

        var dateTimeNow = DateTime.Now;
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.EcdsaSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = dateTimeNow.AddDays(3),
            NotBefore = dateTimeNow,
            SigningCredentials = credentials,
            Issuer = _configuration["Tokens:Issuer"],
            IssuedAt = dateTimeNow.ToUniversalTime(),
            Audience = _configuration["Tokens:Audience"]
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}