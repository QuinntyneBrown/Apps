using Microsoft.EntityFrameworkCore;
using Incomes.Core;

namespace Incomes.Infrastructure.Data;

public class IncomesDbContext : DbContext, IIncomesDbContext
{
    public IncomesDbContext(DbContextOptions<IncomesDbContext> options) : base(options)
    {
    }

    public DbSet<Income> Incomes => Set<Income>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IncomesDbContext).Assembly);
    }
}
