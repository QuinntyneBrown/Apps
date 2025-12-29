using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class StreamingSubscriptionConfiguration : IEntityTypeConfiguration<StreamingSubscription>
{
    public void Configure(EntityTypeBuilder<StreamingSubscription> builder)
    {
        builder.HasKey(s => s.StreamingSubscriptionId);

        builder.Property(s => s.PlatformName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(s => s.SubscriptionTier)
            .HasMaxLength(50);

        builder.Property(s => s.MonthlyCost)
            .HasPrecision(10, 2);

        builder.HasOne(s => s.User)
            .WithMany(u => u.StreamingSubscriptions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
