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
        modelBuilder.Entity<Note>(e => {
            e.ToTable("Notes");
            e.HasKey(x => x.NoteId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
