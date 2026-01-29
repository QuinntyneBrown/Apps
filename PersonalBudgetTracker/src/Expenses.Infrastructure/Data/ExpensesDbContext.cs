using Microsoft.EntityFrameworkCore;
using Expenses.Core;

namespace Expenses.Infrastructure.Data;

public class ExpensesDbContext : DbContext, IExpensesDbContext
{
    public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpensesDbContext).Assembly);
    }
}
