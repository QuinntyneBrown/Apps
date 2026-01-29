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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpensesDbContext).Assembly);
    }
}
