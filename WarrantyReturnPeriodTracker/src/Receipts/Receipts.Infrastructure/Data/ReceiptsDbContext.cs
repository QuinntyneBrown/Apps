using Receipts.Core;
using Receipts.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Receipts.Infrastructure.Data;

public class ReceiptsDbContext : DbContext, IReceiptsDbContext
{
    public ReceiptsDbContext(DbContextOptions<ReceiptsDbContext> options) : base(options) { }
    public DbSet<Receipt> Receipts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Receipt>(entity => { entity.ToTable("Receipts"); entity.HasKey(e => e.ReceiptId); entity.Property(e => e.FileName).HasMaxLength(256).IsRequired(); });
    }
}
