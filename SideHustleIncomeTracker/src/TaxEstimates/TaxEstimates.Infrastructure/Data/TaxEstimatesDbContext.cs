using TaxEstimates.Core;
using TaxEstimates.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TaxEstimates.Infrastructure.Data;

public class TaxEstimatesDbContext : DbContext, ITaxEstimatesDbContext
{
    public TaxEstimatesDbContext(DbContextOptions<TaxEstimatesDbContext> options) : base(options)
    {
    }

    public DbSet<TaxEstimate> TaxEstimates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaxEstimatesDbContext).Assembly);
    }
}
