using Sources.Core;
using Sources.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Sources.Infrastructure.Data;

public class SourcesDbContext : DbContext, ISourcesDbContext
{
    public SourcesDbContext(DbContextOptions<SourcesDbContext> options) : base(options)
    {
    }

    public DbSet<Source> Sources { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Source>(entity =>
        {
            entity.ToTable("Sources");
            entity.HasKey(e => e.SourceId);
        });
    }
}
