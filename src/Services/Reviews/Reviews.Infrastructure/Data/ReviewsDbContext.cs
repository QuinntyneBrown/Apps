using Reviews.Core;
using Reviews.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Reviews.Infrastructure.Data;

public class ReviewsDbContext : DbContext, IReviewsDbContext
{
    public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : base(options)
    {
    }

    public DbSet<Review> Reviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");
            entity.HasKey(e => e.ReviewId);
            entity.HasIndex(e => new { e.TenantId, e.UserId });
            entity.HasIndex(e => e.BookId);
            entity.Property(e => e.Title).HasMaxLength(300);
        });
    }
}
