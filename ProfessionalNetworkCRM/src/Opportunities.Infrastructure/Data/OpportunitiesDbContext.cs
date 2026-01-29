using Opportunities.Core;
using Opportunities.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Opportunities.Infrastructure.Data;

public class OpportunitiesDbContext : DbContext, IOpportunitiesDbContext
{
    public OpportunitiesDbContext(DbContextOptions<OpportunitiesDbContext> options) : base(options) { }

    public DbSet<Opportunity> Opportunitys { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Opportunity>(entity =>
        {
            entity.ToTable("Opportunitys");
            entity.HasKey(e => e.OpportunityId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
