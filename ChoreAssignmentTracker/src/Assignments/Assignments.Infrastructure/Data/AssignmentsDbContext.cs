using Assignments.Core;
using Assignments.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignments.Infrastructure.Data;

public class AssignmentsDbContext : DbContext, IAssignmentsDbContext
{
    public AssignmentsDbContext(DbContextOptions<AssignmentsDbContext> options) : base(options)
    {
    }

    public DbSet<Assignment> Assignments => Set<Assignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.ToTable("Assignments", "assignments");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ChoreId);
            entity.HasIndex(e => e.FamilyMemberId);
            entity.HasIndex(e => e.DueDate);
        });
    }
}
