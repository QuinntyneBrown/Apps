using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class BingeSessionConfiguration : IEntityTypeConfiguration<BingeSession>
{
    public void Configure(EntityTypeBuilder<BingeSession> builder)
    {
        builder.HasKey(b => b.BingeSessionId);

        builder.HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.TVShow)
            .WithMany()
            .HasForeignKey(b => b.TVShowId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
