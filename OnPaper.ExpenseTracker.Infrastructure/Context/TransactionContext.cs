using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Context;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
    {
    }
    
    public DbSet<WorkBook> WorkBooks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}