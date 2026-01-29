using Experiences.Core;
using Experiences.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Experiences.Infrastructure.Data;

public class ExperiencesDbContext : DbContext, IExperiencesDbContext
{
    public ExperiencesDbContext(DbContextOptions<ExperiencesDbContext> options) : base(options)
    {
    }

    public DbSet<Experience> Experiences { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experiences");
            entity.HasKey(e => e.ExperienceId);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.Photos).HasMaxLength(2000);
            entity.Property(e => e.ActualCost).HasPrecision(10, 2);
        });
    }
}
