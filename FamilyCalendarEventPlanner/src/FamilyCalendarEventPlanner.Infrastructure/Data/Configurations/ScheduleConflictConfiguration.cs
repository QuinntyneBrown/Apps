using FamilyCalendarEventPlanner.Core.Model.ConflictAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure.Data.Configurations;

public class ScheduleConflictConfiguration : IEntityTypeConfiguration<ScheduleConflict>
{
    public void Configure(EntityTypeBuilder<ScheduleConflict> builder)
    {
        builder.HasKey(c => c.ConflictId);

        builder.Property(c => c.ConflictId)
            .ValueGeneratedNever();

        builder.Property(c => c.ConflictingEventIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(Guid.Parse)
                    .ToList());

        builder.Property(c => c.AffectedMemberIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(Guid.Parse)
                    .ToList());

        builder.HasIndex(c => c.IsResolved);
    }
}
