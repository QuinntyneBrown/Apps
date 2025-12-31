using PersonalMissionStatementBuilder.Core.Model.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalMissionStatementBuilder.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasIndex(ur => ur.UserId);
        builder.HasIndex(ur => ur.RoleId);
    }
}
