using FamilyCalendarEventPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure;

public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.HasKey(e => e.MemberId);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(320);

        builder.Property(e => e.Color)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Role)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(e => e.FamilyId);
        builder.HasIndex(e => e.Email);
    }
}
