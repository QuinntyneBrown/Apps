using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ViewingMilestoneConfiguration : IEntityTypeConfiguration<ViewingMilestone>
{
    public void Configure(EntityTypeBuilder<ViewingMilestone> builder)
    {
        builder.HasKey(v => v.ViewingMilestoneId);

        builder.Property(v => v.MilestoneType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(v => v.CelebrationTier)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(v => v.ContentBreakdown)
            .HasMaxLength(2000);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
