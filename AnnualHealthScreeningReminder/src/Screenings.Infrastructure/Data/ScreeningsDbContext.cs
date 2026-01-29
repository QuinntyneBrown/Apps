using Screenings.Core;
using Screenings.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Screenings.Infrastructure.Data;

public class ScreeningsDbContext : DbContext, IScreeningsDbContext
{
    public ScreeningsDbContext(DbContextOptions<ScreeningsDbContext> options) : base(options)
    {
    }

    public DbSet<Screening> Screenings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Screening>(entity =>
        {
            entity.ToTable("Screenings");
            entity.HasKey(e => e.ScreeningId);
            entity.Property(e => e.ScreeningType).IsRequired().HasMaxLength(200);
        });
    }
}
