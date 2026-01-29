using ValueAssessments.Core;
using ValueAssessments.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ValueAssessments.Infrastructure.Data;

public class ValueAssessmentsDbContext : DbContext, IValueAssessmentsDbContext
{
    public ValueAssessmentsDbContext(DbContextOptions<ValueAssessmentsDbContext> options) : base(options)
    {
    }

    public DbSet<ValueAssessment> ValueAssessments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ValueAssessment>(entity =>
        {
            entity.ToTable("ValueAssessments");
            entity.HasKey(e => e.AssessmentId);
            entity.Property(e => e.EstimatedValue).HasPrecision(18, 2);
        });
    }
}
