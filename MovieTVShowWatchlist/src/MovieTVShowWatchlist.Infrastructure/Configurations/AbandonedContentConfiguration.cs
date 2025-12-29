using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class AbandonedContentConfiguration : IEntityTypeConfiguration<AbandonedContent>
{
    public void Configure(EntityTypeBuilder<AbandonedContent> builder)
    {
        builder.HasKey(a => a.AbandonedContentId);

        builder.Property(a => a.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.ProgressPercent)
            .HasPrecision(5, 2);

        builder.Property(a => a.QualityRating)
            .HasPrecision(3, 1);

        builder.Property(a => a.AbandonReason)
            .HasMaxLength(500);

        builder.HasIndex(a => a.UserId);
    }
}
