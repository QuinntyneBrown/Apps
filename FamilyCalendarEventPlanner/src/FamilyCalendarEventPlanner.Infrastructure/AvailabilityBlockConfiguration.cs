using FamilyCalendarEventPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure;

public class AvailabilityBlockConfiguration : IEntityTypeConfiguration<AvailabilityBlock>
{
    public void Configure(EntityTypeBuilder<AvailabilityBlock> builder)
    {
        builder.HasKey(e => e.BlockId);

        builder.Property(e => e.BlockType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.Reason)
            .HasMaxLength(500);

        builder.HasIndex(e => e.MemberId);
        builder.HasIndex(e => e.StartTime);
        builder.HasIndex(e => e.EndTime);
    }
}
