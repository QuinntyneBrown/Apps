using Organizations.Core;
using Organizations.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Organizations.Infrastructure.Data;

public class OrganizationsDbContext : DbContext, IOrganizationsDbContext
{
    public OrganizationsDbContext(DbContextOptions<OrganizationsDbContext> options) : base(options)
    {
    }

    public DbSet<Organization> Organizations => Set<Organization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable("Organizations", "organizations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.TaxId).HasMaxLength(50);
            entity.HasIndex(e => e.Name);
        });
    }
}
