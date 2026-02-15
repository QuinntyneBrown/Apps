using JournalEntries.Core;
using JournalEntries.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JournalEntries.Infrastructure.Data;

public class JournalEntriesDbContext : DbContext, IJournalEntriesDbContext
{
    public JournalEntriesDbContext(DbContextOptions<JournalEntriesDbContext> options) : base(options)
    {
    }

    public DbSet<JournalEntry> JournalEntries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.ToTable("JournalEntries");
            entity.HasKey(e => e.JournalEntryId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.HasIndex(e => new { e.TenantId, e.UserId });
        });
    }
}
