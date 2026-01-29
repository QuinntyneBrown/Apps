using Microsoft.EntityFrameworkCore;
using Budgets.Core;

namespace Budgets.Infrastructure.Data;

public class BudgetsDbContext : DbContext, IBudgetsDbContext
{
    public BudgetsDbContext(DbContextOptions<BudgetsDbContext> options) : base(options)
    {
    }

    public DbSet<Budget> Budgets => Set<Budget>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetsDbContext).Assembly);
    }
}
