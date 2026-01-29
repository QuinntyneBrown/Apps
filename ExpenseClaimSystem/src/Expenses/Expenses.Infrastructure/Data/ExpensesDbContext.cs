using Expenses.Core;
using Expenses.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Data;

public class ExpensesDbContext : DbContext, IExpensesDbContext
{
    public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expenses");
            entity.HasKey(e => e.ExpenseId);
            entity.Property(e => e.Description).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.Category).HasMaxLength(100).IsRequired();
            entity.Property(e => e.ReceiptUrl).HasMaxLength(2000);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
