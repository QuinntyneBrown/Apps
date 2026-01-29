using Incomes.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Incomes.Infrastructure.Data.Configurations;

public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Incomes");
        builder.HasKey(i => i.IncomeId);
        builder.Property(i => i.Description).HasMaxLength(500).IsRequired();
        builder.Property(i => i.Amount).HasPrecision(18, 2);
        builder.Property(i => i.Source).HasMaxLength(200);
        builder.HasIndex(i => new { i.TenantId, i.BusinessId });
    }
}
