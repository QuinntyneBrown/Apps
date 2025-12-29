using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class WatchPartyParticipantConfiguration : IEntityTypeConfiguration<WatchPartyParticipant>
{
    public void Configure(EntityTypeBuilder<WatchPartyParticipant> builder)
    {
        builder.HasKey(w => w.WatchPartyParticipantId);

        builder.Property(w => w.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(w => w.User)
            .WithMany()
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
