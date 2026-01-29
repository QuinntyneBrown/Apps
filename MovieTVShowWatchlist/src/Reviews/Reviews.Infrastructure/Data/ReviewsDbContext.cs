using Microsoft.EntityFrameworkCore;
using Reviews.Core;

namespace Reviews.Infrastructure.Data;

public class ReviewsDbContext : DbContext, IReviewsDbContext
{
    public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : base(options)
    {
    }

    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewsDbContext).Assembly);
    }
}
