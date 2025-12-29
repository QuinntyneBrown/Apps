using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class WatchPartyConfiguration : IEntityTypeConfiguration<WatchParty>
{
    public void Configure(EntityTypeBuilder<WatchParty> builder)
    {
        builder.HasKey(w => w.WatchPartyId);

        builder.Property(w => w.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(w => w.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(w => w.Platform)
            .HasMaxLength(100);

        builder.Property(w => w.ViewingContext)
            .HasMaxLength(200);

        builder.Property(w => w.DiscussionPlan)
            .HasMaxLength(2000);

        builder.HasOne(w => w.Host)
            .WithMany()
            .HasForeignKey(w => w.HostUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Participants)
            .WithOne(p => p.WatchParty)
            .HasForeignKey(p => p.WatchPartyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
