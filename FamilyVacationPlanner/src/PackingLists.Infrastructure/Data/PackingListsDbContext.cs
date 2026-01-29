using PackingLists.Core;
using PackingLists.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PackingLists.Infrastructure.Data;

public class PackingListsDbContext : DbContext, IPackingListsDbContext
{
    public PackingListsDbContext(DbContextOptions<PackingListsDbContext> options) : base(options)
    {
    }

    public DbSet<PackingList> PackingLists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PackingList>(entity =>
        {
            entity.ToTable("PackingLists");
            entity.HasKey(e => e.PackingListId);
            entity.Property(e => e.ItemName).IsRequired().HasMaxLength(200);
        });
    }
}
