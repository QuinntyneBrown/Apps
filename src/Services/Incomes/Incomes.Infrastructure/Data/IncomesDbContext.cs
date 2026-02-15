using Incomes.Core;
using Incomes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Incomes.Infrastructure.Data;

public class IncomesDbContext : DbContext, IIncomesDbContext
{
    public IncomesDbContext(DbContextOptions<IncomesDbContext> options) : base(options)
    {
    }

    public DbSet<Income> Incomes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IncomesDbContext).Assembly);
    }
}
