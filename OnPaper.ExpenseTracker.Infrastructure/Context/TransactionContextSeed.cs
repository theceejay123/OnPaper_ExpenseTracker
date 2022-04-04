using System.Reflection;
using Microsoft.Extensions.Logging;

namespace OnPaper.ExpenseTracker.Infrastructure.Context;

public class TransactionContextSeed
{
    public static async Task SeedAsync(TransactionContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!context.WorkBooks.Any())
            {
                
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<TransactionContextSeed>();
            logger.LogError(e.Message);
        }
    }
}