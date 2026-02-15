using Lessons.Core;
using Lessons.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Lessons.Infrastructure.Data;

public class LessonsDbContext : DbContext, ILessonsDbContext
{
    public LessonsDbContext(DbContextOptions<LessonsDbContext> options) : base(options)
    {
    }

    public DbSet<Lesson> Lessons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.ToTable("Lessons");
            entity.HasKey(e => e.LessonId);
        });
    }
}
