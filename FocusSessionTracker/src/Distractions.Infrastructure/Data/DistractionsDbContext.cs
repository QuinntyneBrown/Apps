using Distractions.Core;
using Distractions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Distractions.Infrastructure.Data;

public class DistractionsDbContext : DbContext, IDistractionsDbContext
{
    public DistractionsDbContext(DbContextOptions<DistractionsDbContext> options) : base(options)
    {
    }

    public DbSet<Distraction> Distractions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Distraction>(entity =>
        {
            entity.ToTable("Distractions");
            entity.HasKey(e => e.DistractionId);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.SessionId);
            entity.HasIndex(e => e.TenantId);
        });
    }
}
