using Skills.Core;
using Skills.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Skills.Infrastructure.Data;

public class SkillsDbContext : DbContext, ISkillsDbContext
{
    public SkillsDbContext(DbContextOptions<SkillsDbContext> options) : base(options)
    {
    }

    public DbSet<Skill> Skills { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skills");
            entity.HasKey(e => e.SkillId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.YearsOfExperience).HasPrecision(5, 2);
        });
    }
}
