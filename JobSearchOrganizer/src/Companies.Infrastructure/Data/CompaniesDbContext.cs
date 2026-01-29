using Companies.Core;
using Companies.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Data;

public class CompaniesDbContext : DbContext, ICompaniesDbContext
{
    public CompaniesDbContext(DbContextOptions<CompaniesDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Companies");
            entity.HasKey(e => e.CompanyId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        });
    }
}
