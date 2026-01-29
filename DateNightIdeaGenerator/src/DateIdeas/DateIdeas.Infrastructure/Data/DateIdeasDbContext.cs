using DateIdeas.Core;
using DateIdeas.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DateIdeas.Infrastructure.Data;

public class DateIdeasDbContext : DbContext, IDateIdeasDbContext
{
    public DateIdeasDbContext(DbContextOptions<DateIdeasDbContext> options) : base(options)
    {
    }

    public DbSet<DateIdea> DateIdeas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DateIdea>(entity =>
        {
            entity.ToTable("DateIdeas");
            entity.HasKey(e => e.DateIdeaId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EstimatedCost).HasPrecision(10, 2);
        });
    }
}
