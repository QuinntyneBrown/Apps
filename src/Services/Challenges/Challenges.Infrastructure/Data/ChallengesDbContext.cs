using Challenges.Core; using Challenges.Core.Models; using Microsoft.EntityFrameworkCore;
namespace Challenges.Infrastructure.Data;
public class ChallengesDbContext : DbContext, IChallengesDbContext { public ChallengesDbContext(DbContextOptions<ChallengesDbContext> options) : base(options) { } public DbSet<Challenge> Challenges { get; set; } = null!;
protected override void OnModelCreating(ModelBuilder modelBuilder) { base.OnModelCreating(modelBuilder); modelBuilder.Entity<Challenge>(entity => { entity.ToTable("Challenges"); entity.HasKey(e => e.ChallengeId); }); } }
