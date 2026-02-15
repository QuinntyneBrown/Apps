using Carpools.Core;
using Carpools.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Carpools.Infrastructure.Data;

public class CarpoolsDbContext : DbContext, ICarpoolsDbContext
{
    public CarpoolsDbContext(DbContextOptions<CarpoolsDbContext> options) : base(options)
    {
    }

    public DbSet<Carpool> Carpools { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Carpool>(entity =>
        {
            entity.ToTable("Carpools");
            entity.HasKey(e => e.CarpoolId);
            entity.Property(e => e.DriverName).IsRequired().HasMaxLength(200);
        });
    }
}
