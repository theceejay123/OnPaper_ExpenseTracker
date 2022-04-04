using HashidsNet;
using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Infrastructure.Context;
using OnPaper.ExpenseTracker.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHashids>(_ => new Hashids(builder.Configuration.GetSection("HashIds:PepperKey").Value));
builder.Services.AddDbContext<TransactionContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApiServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
