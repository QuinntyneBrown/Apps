using Businesses.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Businesses.Infrastructure.Data.Configurations;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.ToTable("Businesses");
        builder.HasKey(b => b.BusinessId);
        builder.Property(b => b.Name).HasMaxLength(200).IsRequired();
        builder.Property(b => b.Description).HasMaxLength(1000);
        builder.Property(b => b.Category).HasMaxLength(100);
        builder.HasIndex(b => new { b.TenantId, b.UserId });
    }
}
