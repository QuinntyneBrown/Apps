using Memories.Core;
using Memories.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Memories.Infrastructure.Data;

public class MemoriesDbContext : DbContext, IMemoriesDbContext
{
    public MemoriesDbContext(DbContextOptions<MemoriesDbContext> options) : base(options)
    {
    }

    public DbSet<Memory> Memories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Memory>(entity =>
        {
            entity.ToTable("Memories");
            entity.HasKey(e => e.MemoryId);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.HasIndex(e => e.BucketListItemId);
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Location).HasMaxLength(300);
        });
    }
}
