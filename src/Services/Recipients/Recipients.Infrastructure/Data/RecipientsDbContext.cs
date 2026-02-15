using Recipients.Core;
using Recipients.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Recipients.Infrastructure.Data;

public class RecipientsDbContext : DbContext, IRecipientsDbContext
{
    public RecipientsDbContext(DbContextOptions<RecipientsDbContext> options) : base(options)
    {
    }

    public DbSet<Recipient> Recipients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recipient>(entity =>
        {
            entity.ToTable("Recipients");
            entity.HasKey(e => e.RecipientId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
