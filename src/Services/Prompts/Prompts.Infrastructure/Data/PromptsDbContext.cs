using Prompts.Core;
using Prompts.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Prompts.Infrastructure.Data;

public class PromptsDbContext : DbContext, IPromptsDbContext
{
    public PromptsDbContext(DbContextOptions<PromptsDbContext> options) : base(options)
    {
    }

    public DbSet<Prompt> Prompts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Prompt>(entity =>
        {
            entity.ToTable("Prompts");
            entity.HasKey(e => e.PromptId);
            entity.Property(e => e.Text).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
        });
    }
}
