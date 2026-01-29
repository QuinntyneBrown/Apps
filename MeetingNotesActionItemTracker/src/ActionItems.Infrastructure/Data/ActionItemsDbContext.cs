using ActionItems.Core;
using ActionItems.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ActionItems.Infrastructure.Data;

public class ActionItemsDbContext : DbContext, IActionItemsDbContext
{
    public ActionItemsDbContext(DbContextOptions<ActionItemsDbContext> options) : base(options) { }
    public DbSet<ActionItem> ActionItems { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionItem>(e => {
            e.ToTable("ActionItems");
            e.HasKey(x => x.ActionItemId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
