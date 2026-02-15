using TaxEstimates.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxEstimates.Infrastructure.Data.Configurations;

public class TaxEstimateConfiguration : IEntityTypeConfiguration<TaxEstimate>
{
    public void Configure(EntityTypeBuilder<TaxEstimate> builder)
    {
        builder.ToTable("TaxEstimates");
        builder.HasKey(t => t.TaxEstimateId);
        builder.Property(t => t.TotalIncome).HasPrecision(18, 2);
        builder.Property(t => t.TotalExpenses).HasPrecision(18, 2);
        builder.Property(t => t.NetIncome).HasPrecision(18, 2);
        builder.Property(t => t.EstimatedTax).HasPrecision(18, 2);
        builder.Property(t => t.TaxRate).HasPrecision(5, 2);
        builder.HasIndex(t => new { t.TenantId, t.UserId, t.Year, t.Quarter });
    }
}
