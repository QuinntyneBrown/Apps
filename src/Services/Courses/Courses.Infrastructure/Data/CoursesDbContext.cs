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
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoursesDbContext).Assembly);
    }
}
