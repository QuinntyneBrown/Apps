using Loans.Core;
using Loans.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Loans.Infrastructure.Data;

public class LoansDbContext : DbContext, ILoansDbContext
{
    public LoansDbContext(DbContextOptions<LoansDbContext> options) : base(options)
    {
    }

    public DbSet<Loan> Loans { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.ToTable("Loans");
            entity.HasKey(e => e.LoanId);
            entity.Property(e => e.RequestedAmount).HasPrecision(18, 2);
        });
    }
}
