using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ViewingRecordConfiguration : IEntityTypeConfiguration<ViewingRecord>
{
    public void Configure(EntityTypeBuilder<ViewingRecord> builder)
    {
        builder.HasKey(v => v.ViewingRecordId);

        builder.Property(v => v.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(v => v.ViewingPlatform)
            .HasMaxLength(100);

        builder.Property(v => v.ViewingLocation)
            .HasMaxLength(200);

        builder.Property(v => v.ViewingContext)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(v => new { v.UserId, v.WatchDate });
        builder.HasIndex(v => v.ContentId);

        builder.HasOne(v => v.User)
            .WithMany(u => u.ViewingRecords)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Companions)
            .WithOne(c => c.ViewingRecord)
            .HasForeignKey(c => c.ViewingRecordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
