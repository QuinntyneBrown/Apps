using Interviews.Core;
using Interviews.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Interviews.Infrastructure.Data;

public class InterviewsDbContext : DbContext, IInterviewsDbContext
{
    public InterviewsDbContext(DbContextOptions<InterviewsDbContext> options) : base(options)
    {
    }

    public DbSet<Interview> Interviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.ToTable("Interviews");
            entity.HasKey(e => e.InterviewId);
            entity.Property(e => e.Type).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
        });
    }
}
