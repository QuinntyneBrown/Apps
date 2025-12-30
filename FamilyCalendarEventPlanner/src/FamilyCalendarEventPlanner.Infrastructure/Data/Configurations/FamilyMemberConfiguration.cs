using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyCalendarEventPlanner.Infrastructure.Data.Configurations;

public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.HasKey(m => m.MemberId);

        builder.Property(m => m.MemberId)
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Email)
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(m => m.Color)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.IsImmediate)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(m => m.RelationType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(m => m.FamilyId);
        builder.HasIndex(m => m.Email);
        builder.HasIndex(m => m.IsImmediate);
    }
}
