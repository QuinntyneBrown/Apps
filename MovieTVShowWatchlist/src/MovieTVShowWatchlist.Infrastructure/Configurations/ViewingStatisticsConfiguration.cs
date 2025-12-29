using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ViewingStatisticsConfiguration : IEntityTypeConfiguration<ViewingStatistics>
{
    public void Configure(EntityTypeBuilder<ViewingStatistics> builder)
    {
        builder.HasKey(v => v.ViewingStatisticsId);

        builder.Property(v => v.Period)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(v => v.GenreBreakdown)
            .HasMaxLength(2000);

        builder.Property(v => v.PlatformBreakdown)
            .HasMaxLength(2000);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
