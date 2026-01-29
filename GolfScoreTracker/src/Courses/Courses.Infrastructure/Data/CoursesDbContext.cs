using Courses.Core;
using Courses.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Data;

public class CoursesDbContext : DbContext, ICoursesDbContext
{
    public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options) { }
    public DbSet<Course> Courses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(e => { e.ToTable("Courses"); e.HasKey(c => c.CourseId); e.Property(c => c.Name).IsRequired().HasMaxLength(200); e.Property(c => c.CourseRating).HasPrecision(4, 1); });
    }
}
