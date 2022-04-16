using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnPaper.ExpenseTracker.Core.Models.Identity;
using OnPaper.ExpenseTracker.Infrastructure.Context;

namespace OnPaper.ExpenseTracker.WebApi.Extensions;

public static class IdentityDbServicesExtensions
{
    public static IServiceCollection AddIdentityDbServices(this IServiceCollection services, IConfiguration config)
    {
        var builder = services.AddIdentityCore<AppUser>();
        builder = new IdentityBuilder(builder.UserType, builder.Services);
        builder.AddEntityFrameworkStores<AppIdentityDbContext>();
        builder.AddSignInManager<SignInManager<AppUser>>();

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = GetECDsaSecurityKey(),
            ValidIssuer = config["Tokens:Issuer"],
            ValidAudiences = new []
            {
                config["Tokens:Issuer"], 
                config["Tokens:Audience"]
            },
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = tokenValidationParameters;
            options.TokenValidationParameters.ValidTypes = new[] {"JWT"};
            options.TokenValidationParameters.ValidAlgorithms = new[] {"ES256"};
            options.Validate();
        });

        return services;
    }

    private static ECDsaSecurityKey GetECDsaSecurityKey()
    {
        var privateKey = File.ReadAllText("../../_keys/private_key.pem");
        var generatedEcdsa = ECDsa.Create();
        generatedEcdsa.ImportFromPem(privateKey);
        return new ECDsaSecurityKey(generatedEcdsa);
    }
}