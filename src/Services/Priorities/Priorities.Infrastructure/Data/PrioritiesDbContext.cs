using Priorities.Core; using Priorities.Core.Models; using Microsoft.EntityFrameworkCore;
namespace Priorities.Infrastructure.Data;
public class PrioritiesDbContext : DbContext, IPrioritiesDbContext { public PrioritiesDbContext(DbContextOptions<PrioritiesDbContext> options) : base(options) { } public DbSet<Priority> Priorities { get; set; } = null!;
protected override void OnModelCreating(ModelBuilder modelBuilder) { base.OnModelCreating(modelBuilder); modelBuilder.Entity<Priority>(entity => { entity.ToTable("Priorities"); entity.HasKey(e => e.PriorityId); entity.Property(e => e.Title).HasMaxLength(200).IsRequired(); }); } }
