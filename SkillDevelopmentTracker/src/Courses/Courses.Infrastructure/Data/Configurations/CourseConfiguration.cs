using Courses.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Infrastructure.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.CourseId);
        builder.Property(c => c.Title).HasMaxLength(300).IsRequired();
        builder.Property(c => c.Provider).HasMaxLength(200);
        builder.Property(c => c.Url).HasMaxLength(500);
        builder.HasIndex(c => new { c.TenantId, c.UserId });
    }
}
