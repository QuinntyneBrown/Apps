using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ViewingCompanionConfiguration : IEntityTypeConfiguration<ViewingCompanion>
{
    public void Configure(EntityTypeBuilder<ViewingCompanion> builder)
    {
        builder.HasKey(v => v.ViewingCompanionId);

        builder.Property(v => v.CompanionName)
            .IsRequired()
            .HasMaxLength(200);
    }
}
