using Notes.Core;
using Notes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Notes.Infrastructure.Data;

public class NotesDbContext : DbContext, INotesDbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

    public DbSet<Note> Notes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Note>(entity =>
        {
            entity.ToTable("Notes");
            entity.HasKey(e => e.NoteId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
