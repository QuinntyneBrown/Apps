using Interactions.Core;
using Interactions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Interactions.Infrastructure.Data;

public class InteractionsDbContext : DbContext, IInteractionsDbContext
{
    public InteractionsDbContext(DbContextOptions<InteractionsDbContext> options) : base(options) { }

    public DbSet<Interaction> Interactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Interaction>(entity =>
        {
            entity.ToTable("Interactions");
            entity.HasKey(e => e.InteractionId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
