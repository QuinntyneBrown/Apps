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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkillsDbContext).Assembly);
    }
}
