using Budgets.Core;
using Budgets.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgets.Infrastructure.Data;

public class BudgetsDbContext : DbContext, IBudgetsDbContext
{
    public BudgetsDbContext(DbContextOptions<BudgetsDbContext> options) : base(options)
    {
    }

    public DbSet<VacationBudget> VacationBudgets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VacationBudget>(entity =>
        {
            entity.ToTable("VacationBudgets");
            entity.HasKey(e => e.VacationBudgetId);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.AllocatedAmount).HasPrecision(18, 2);
            entity.Property(e => e.SpentAmount).HasPrecision(18, 2);
        });
    }
}
