using Notes.Core;
using Notes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Notes.Infrastructure.Data;

public class NotesDbContext : DbContext, INotesDbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>(entity =>
        {
            entity.ToTable("Notes", "notes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Tags).HasMaxLength(500);
            entity.HasIndex(e => e.EventId);
            entity.HasIndex(e => e.TenantId);
        });
    }
}
