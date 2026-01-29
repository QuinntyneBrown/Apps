using Introductions.Core;
using Introductions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Introductions.Infrastructure.Data;

public class IntroductionsDbContext : DbContext, IIntroductionsDbContext
{
    public IntroductionsDbContext(DbContextOptions<IntroductionsDbContext> options) : base(options) { }

    public DbSet<Introduction> Introductions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Introduction>(entity =>
        {
            entity.ToTable("Introductions");
            entity.HasKey(e => e.IntroductionId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
