using Microsoft.EntityFrameworkCore;
using Ratings.Core;

namespace Ratings.Infrastructure.Data;

public class RatingsDbContext : DbContext, IRatingsDbContext
{
    public RatingsDbContext(DbContextOptions<RatingsDbContext> options) : base(options)
    {
    }

    public DbSet<Rating> Ratings => Set<Rating>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RatingsDbContext).Assembly);
    }
}
