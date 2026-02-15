using Topics.Core;
using Topics.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Topics.Infrastructure.Data;

public class TopicsDbContext : DbContext, ITopicsDbContext
{
    public TopicsDbContext(DbContextOptions<TopicsDbContext> options) : base(options) { }
    public DbSet<Topic> Topics { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topic>(e => {
            e.ToTable("Topics");
            e.HasKey(x => x.TopicId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
