using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Context;

public class TransactionContextSeed
{
    public static async Task SeedAsync(TransactionContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            // var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!context.TransactionTypes.Any())
            {
                var transactionTypesData = await File.ReadAllTextAsync("../OnPaper.ExpenseTracker.Infrastructure/Data/Seed/transactionTypes.json");
                var transactionTypes = JsonSerializer.Deserialize<List<TransactionType>>(transactionTypesData);
                foreach (var transactionType in transactionTypes)
                {
                    context.TransactionTypes.Add(transactionType);
                }
                await context.SaveChangesAsync();
            }
            if (!context.PaymentTypes.Any())
            {
                var paymentTypesData = await File.ReadAllTextAsync("../OnPaper.ExpenseTracker.Infrastructure/Data/Seed/paymentTypes.json");
                var paymentTypes = JsonSerializer.Deserialize<List<PaymentType>>(paymentTypesData);
                foreach (var paymentType in paymentTypes)
                {
                    context.PaymentTypes.Add(paymentType);
                }
                await context.SaveChangesAsync();
            }
            if (!context.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync("../OnPaper.ExpenseTracker.Infrastructure/Data/Seed/categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<TransactionContextSeed>();
            logger.LogError(e.Message);
        }
    }
}