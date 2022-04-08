using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Infrastructure.Data.Config;

public class WorkBookConfiguration : IEntityTypeConfiguration<WorkBook>
{
    public void Configure(EntityTypeBuilder<WorkBook> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.HasMany(x => x.Transactions).WithOne(o => o.WorkBook).HasForeignKey(f => f.WorkBookId).OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.CreateDate).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        builder.Property(p => p.UpdateDate).HasDefaultValueSql("now()").ValueGeneratedOnUpdate();
    }
}