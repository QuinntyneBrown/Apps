using Businesses.Core;
using Businesses.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Businesses.Infrastructure.Data;

public class BusinessesDbContext : DbContext, IBusinessesDbContext
{
    public BusinessesDbContext(DbContextOptions<BusinessesDbContext> options) : base(options)
    {
    }

    public DbSet<Business> Businesses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessesDbContext).Assembly);
    }
}
