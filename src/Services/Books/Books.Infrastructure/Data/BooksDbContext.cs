using Books.Core;
using Books.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Data;

public class BooksDbContext : DbContext, IBooksDbContext
{
    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.HasKey(e => e.BookId);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Author).HasMaxLength(300);
            entity.Property(e => e.Isbn).HasMaxLength(20);
            entity.Property(e => e.Genre).HasMaxLength(100);
        });
    }
}
