using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Data.Config;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.HasOne(p => p.PaymentType).WithMany().HasForeignKey(p => p.PaymentTypeId);
        builder.HasOne(p => p.TransactionType).WithMany().HasForeignKey(p => p.TransactionTypeId);
        builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
    }
}