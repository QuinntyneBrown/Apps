using FollowUps.Core;
using FollowUps.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FollowUps.Infrastructure.Data;

public class FollowUpsDbContext : DbContext, IFollowUpsDbContext
{
    public FollowUpsDbContext(DbContextOptions<FollowUpsDbContext> options) : base(options) { }

    public DbSet<FollowUp> FollowUps { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<FollowUp>(entity =>
        {
            entity.ToTable("FollowUps");
            entity.HasKey(e => e.FollowUpId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
