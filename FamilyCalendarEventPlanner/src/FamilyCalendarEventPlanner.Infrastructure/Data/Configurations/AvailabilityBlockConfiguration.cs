using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure.Data.Configurations;

public class AvailabilityBlockConfiguration : IEntityTypeConfiguration<AvailabilityBlock>
{
    public void Configure(EntityTypeBuilder<AvailabilityBlock> builder)
    {
        builder.HasKey(b => b.BlockId);

        builder.Property(b => b.BlockId)
            .ValueGeneratedNever();

        builder.Property(b => b.Reason)
            .HasMaxLength(500);

        builder.HasIndex(b => b.MemberId);
        builder.HasIndex(b => b.StartTime);
        builder.HasIndex(b => b.EndTime);
    }
}
