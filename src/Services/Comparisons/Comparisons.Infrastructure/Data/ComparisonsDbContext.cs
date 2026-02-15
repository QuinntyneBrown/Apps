using Microsoft.EntityFrameworkCore;
using Comparisons.Core;

namespace Comparisons.Infrastructure.Data;

public class ComparisonsDbContext : DbContext, IComparisonsDbContext
{
    public ComparisonsDbContext(DbContextOptions<ComparisonsDbContext> options) : base(options)
    {
    }

    public DbSet<Comparison> Comparisons => Set<Comparison>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComparisonsDbContext).Assembly);
    }
}
