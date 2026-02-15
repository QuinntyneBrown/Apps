using Rounds.Core;
using Rounds.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Rounds.Infrastructure.Data;

public class RoundsDbContext : DbContext, IRoundsDbContext
{
    public RoundsDbContext(DbContextOptions<RoundsDbContext> options) : base(options) { }
    public DbSet<Round> Rounds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Round>(e => { e.ToTable("Rounds"); e.HasKey(r => r.RoundId); e.HasIndex(r => new { r.TenantId, r.UserId, r.PlayedDate }); });
    }
}
