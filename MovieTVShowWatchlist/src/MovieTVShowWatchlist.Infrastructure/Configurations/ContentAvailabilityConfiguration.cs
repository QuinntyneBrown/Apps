using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ContentAvailabilityConfiguration : IEntityTypeConfiguration<ContentAvailability>
{
    public void Configure(EntityTypeBuilder<ContentAvailability> builder)
    {
        builder.HasKey(c => c.ContentAvailabilityId);

        builder.Property(c => c.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(c => c.Platform)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.AvailabilityWindow)
            .HasMaxLength(200);

        builder.Property(c => c.RegionalRestrictions)
            .HasMaxLength(500);

        builder.HasIndex(c => c.ContentId);
    }
}
