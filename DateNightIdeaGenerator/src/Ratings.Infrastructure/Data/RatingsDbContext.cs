using Ratings.Core;
using Ratings.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Ratings.Infrastructure.Data;

public class RatingsDbContext : DbContext, IRatingsDbContext
{
    public RatingsDbContext(DbContextOptions<RatingsDbContext> options) : base(options)
    {
    }

    public DbSet<Rating> Ratings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("Ratings");
            entity.HasKey(e => e.RatingId);
            entity.Property(e => e.Score).IsRequired();
            entity.Property(e => e.Review).HasMaxLength(2000);
        });
    }
}
