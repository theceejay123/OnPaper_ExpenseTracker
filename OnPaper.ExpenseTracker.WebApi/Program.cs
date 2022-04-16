using AspNetCore.Hashids.Mvc;
using HashidsNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Core.Models.Identity;
using OnPaper.ExpenseTracker.Infrastructure.Context;
using OnPaper.ExpenseTracker.WebApi.Extensions;
using OnPaper.ExpenseTracker.WebApi.MappingProfiles;
using OnPaper.ExpenseTracker.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<TransactionContext>(context => context.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppIdentityDbContext>(context => context.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

// Add services
builder.Services.AddHashids(x =>
{
    x.Salt = builder.Configuration.GetSection("HashIds:PepperKey").Value;
    x.MinHashLength = 11;
});
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddIdentityDbServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", p =>
    {
        p.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDocumentation();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<TransactionContext>();
        await context.Database.MigrateAsync();
        await TransactionContextSeed.SeedAsync(context, loggerFactory);

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var identityContext = services.GetRequiredService<AppIdentityDbContext>();
        await identityContext.Database.MigrateAsync();
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migrations.");
    }
}

app.Run();