using System.Text.Json;
using FamilyCalendarEventPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure;

public class ScheduleConflictConfiguration : IEntityTypeConfiguration<ScheduleConflict>
{
    public void Configure(EntityTypeBuilder<ScheduleConflict> builder)
    {
        builder.HasKey(e => e.ConflictId);

        builder.Property(e => e.ConflictingEventIds)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new List<Guid>())
            .HasMaxLength(4000);

        builder.Property(e => e.AffectedMemberIds)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new List<Guid>())
            .HasMaxLength(4000);

        builder.Property(e => e.ConflictSeverity)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(e => e.IsResolved);
        builder.HasIndex(e => e.ConflictSeverity);
    }
}
