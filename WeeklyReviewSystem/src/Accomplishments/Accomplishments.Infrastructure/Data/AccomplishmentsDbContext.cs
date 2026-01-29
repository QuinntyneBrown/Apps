using Accomplishments.Core; using Accomplishments.Core.Models; using Microsoft.EntityFrameworkCore;
namespace Accomplishments.Infrastructure.Data;
public class AccomplishmentsDbContext : DbContext, IAccomplishmentsDbContext { public AccomplishmentsDbContext(DbContextOptions<AccomplishmentsDbContext> options) : base(options) { } public DbSet<Accomplishment> Accomplishments { get; set; } = null!;
protected override void OnModelCreating(ModelBuilder modelBuilder) { base.OnModelCreating(modelBuilder); modelBuilder.Entity<Accomplishment>(entity => { entity.ToTable("Accomplishments"); entity.HasKey(e => e.AccomplishmentId); }); } }
