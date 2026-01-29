using Chores.Core;
using Chores.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Chores.Infrastructure.Data;

public class ChoresDbContext : DbContext, IChoresDbContext
{
    public ChoresDbContext(DbContextOptions<ChoresDbContext> options) : base(options)
    {
    }

    public DbSet<Chore> Chores => Set<Chore>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Chore>(entity =>
        {
            entity.ToTable("Chores", "chores");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.HasIndex(e => e.Name);
        });
    }
}
