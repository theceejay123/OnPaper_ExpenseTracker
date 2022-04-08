using System.Text;
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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:SecurityKey"])),
                ValidIssuer = config["Tokens:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = true
            };
        });

        return services;
    }
}