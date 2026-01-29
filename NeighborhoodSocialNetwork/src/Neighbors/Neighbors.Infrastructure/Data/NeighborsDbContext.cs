using Microsoft.EntityFrameworkCore;
using Neighbors.Core;

namespace Neighbors.Infrastructure.Data;

public class NeighborsDbContext : DbContext, INeighborsDbContext
{
    public NeighborsDbContext(DbContextOptions<NeighborsDbContext> options) : base(options)
    {
    }

    public DbSet<Neighbor> Neighbors => Set<Neighbor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NeighborsDbContext).Assembly);
    }
}
