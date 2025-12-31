using PokerGameTracker.Core.Model.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PokerGameTracker.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.RoleId);

        builder.Property(r => r.RoleId)
            .ValueGeneratedNever();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(r => r.Name)
            .IsUnique();
    }
}
