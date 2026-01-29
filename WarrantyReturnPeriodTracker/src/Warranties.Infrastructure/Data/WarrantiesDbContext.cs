using Warranties.Core;
using Warranties.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Warranties.Infrastructure.Data;

public class WarrantiesDbContext : DbContext, IWarrantiesDbContext
{
    public WarrantiesDbContext(DbContextOptions<WarrantiesDbContext> options) : base(options) { }
    public DbSet<Warranty> Warranties { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Warranty>(entity => { entity.ToTable("Warranties"); entity.HasKey(e => e.WarrantyId); entity.Property(e => e.WarrantyType).HasMaxLength(100); });
    }
}
