using Groups.Core;
using Groups.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Groups.Infrastructure.Data;

public class GroupsDbContext : DbContext, IGroupsDbContext
{
    public GroupsDbContext(DbContextOptions<GroupsDbContext> options) : base(options) { }
    public DbSet<Group> Groups { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(e => {
            e.ToTable("Groups");
            e.HasKey(x => x.GroupId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
