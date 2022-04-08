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

// Add services to the container.
builder.Services.AddSingleton<IHashids>(_ => new Hashids(builder.Configuration.GetSection("HashIds:PepperKey").Value));
builder.Services.AddDbContext<TransactionContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppIdentityDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddApiServices();
builder.Services.AddIdentityDbServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", p =>
    {
        p.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});

var app = builder.Build();

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
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "An error occured during migration.");
    }
}

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

await app.RunAsync();
